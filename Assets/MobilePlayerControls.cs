using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MobilePlayerControls : MonoBehaviour {

	enum FingerAction {
		off, shortTap, longTap, swipe, joystick
	}
	private FingerAction rightFingerAction;

	private CharacterController controller;
	private float speed = 20F;
	public float leftJoystickRange = 200;
    public float rightJoystickRange = 300;
	public float tapLength = 0.3f;
    private float jumpSpeed = 15.0F;
    private float gravity = 9.8F;
    private Vector3 moveDirection = Vector3.zero;
	private TouchPositionsDict touchPositionsDict;
	private float leftMinDragDistance;
	private float rightMinDragDistance;
    private float leftFingerHorizontal;
    private float leftFingerVertical;
    private float rightFingerHorizontal;
    private float rightFingerVertical;
	private bool isMobile;

	void Start () {
		// isMobile = (Application.platform == RuntimePlatform.Android);
        isMobile = true;
        Debug.Log(isMobile);

		controller = GetComponent<CharacterController>();
		leftMinDragDistance = 5;
		rightMinDragDistance = 20;
		touchPositionsDict = new TouchPositionsDict();
		rightFingerAction = FingerAction.off;
	}

	void FixedUpdate() {
		if (isMobile) {
			if (controller.isGrounded) {
				moveDirection = new Vector3(leftFingerHorizontal, 0, leftFingerVertical);
				moveDirection *= speed;
				if (rightFingerAction == FingerAction.shortTap) {
					rightFingerAction = FingerAction.off;
					moveDirection.y = jumpSpeed;
					Debug.Log("jumped");
				}
				moveDirection = transform.TransformDirection(moveDirection);
			}
			moveDirection.y -= gravity * Time.deltaTime;
			controller.Move(moveDirection * Time.deltaTime);
		}
	}

    void Update() {
		// Debug.Log(isLocalPlayer);
		// if (!isLocalPlayer) {
        //     return;
        // }
        Debug.Log("update hit");
		if (isMobile) {
			ReadTouches();
		} else {
			var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
			var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;
			transform.Rotate(0, x, 0);
			transform.Translate(0, 0, z);
		}
    }

	void ReadTouches() {
		foreach (Touch touch in Input.touches) {
			Debug.Log("touched");
			if (touch.phase == TouchPhase.Began) {
                touchPositionsDict.AddFingerPosition(
                    touch.fingerId,
                    touch.position,
                    Time.time
                );
            }
            else if (touch.phase == TouchPhase.Moved) {
                CheckMovementJoysticks(touch);
            }
            else if (touch.phase == TouchPhase.Ended) {
				float minDistance;
				bool isLeft = touchPositionsDict.GetOnLeftForId(touch.fingerId);
				if (isLeft) {
					leftFingerHorizontal = 0;
                	leftFingerVertical = 0;
					minDistance = leftMinDragDistance;
				} else {
					rightFingerHorizontal = 0;
                	rightFingerVertical = 0;
					minDistance = rightMinDragDistance;
				}
                Vector3 firstPos = touchPositionsDict.GetFirstForId(touch.fingerId);
                Vector3 lastPos = touch.position;
                float startTime = touchPositionsDict.GetStartTimeForId(touch.fingerId);
				
                if (IsSwipeDistanceLargeEnough(firstPos, lastPos, minDistance)) {
                    CheckTypeOfSwipe(firstPos, lastPos, touch.fingerId);
                } else {
                    CheckTypeOfTap(startTime);
                }
            }
		}
	}

	void CheckMovementJoysticks(Touch touch) {
        Vector3 firstPos = touchPositionsDict.GetFirstForId(touch.fingerId);
        Vector3 lastPos = touch.position;
		float minDistance = touchPositionsDict.GetOnLeftForId(touch.fingerId) ? leftMinDragDistance : rightMinDragDistance;
        if (IsSwipeDistanceLargeEnough(firstPos, lastPos, minDistance)) {
            Vector2 joystickVec;
            if (touchPositionsDict.GetOnLeftForId(touch.fingerId)) {
                joystickVec = CheckDirectionOfJoystick(firstPos, lastPos, touch.fingerId, leftJoystickRange);
                leftFingerHorizontal = joystickVec.x;
                leftFingerVertical = joystickVec.y;
            } else {
                joystickVec = CheckDirectionOfJoystick(firstPos, lastPos, touch.fingerId, rightJoystickRange);
                rightFingerHorizontal = joystickVec.x;
                rightFingerVertical = joystickVec.y;
            }
        }
    }

    Vector2 CheckDirectionOfJoystick(Vector3 first, Vector3 last, int fingerId, float joystickRange) {
        float horizontal = Mathf.Clamp((last.x - first.x) / joystickRange, -1, 1);
        float vertical = Mathf.Clamp((last.y - first.y) / joystickRange, -1, 1);
        return new Vector2(horizontal, vertical);
    }

    bool IsSwipeDistanceLargeEnough(Vector3 first, Vector3 last, float minDragDistance) {
        return Mathf.Abs(last.x - first.x) > minDragDistance || Mathf.Abs(last.y - first.y) > minDragDistance;
    }

	    void CheckTypeOfSwipe(Vector3 first, Vector3 last, int fingerId) {
        if (Mathf.Abs(last.x - first.x) > Mathf.Abs(last.y - first.y)) {
            if ((last.x > first.x)) {
                Debug.Log("Right Swipe for " + fingerId);
            }
            else {
                Debug.Log("Left Swipe for " + fingerId);
            }
        }
        else {
            if (last.y > first.y) {
                Debug.Log("Up Swipe for " + fingerId);
            }
            else {
                Debug.Log("Down Swipe for " + fingerId);
            }
        }
    }
    void CheckTypeOfTap(float start) {
        if (Time.time - start > tapLength) {
            Debug.Log("long tap");
			rightFingerAction = FingerAction.longTap;
        } else {
            Debug.Log("Short tap");
			rightFingerAction = FingerAction.shortTap;
        }
    }
}
