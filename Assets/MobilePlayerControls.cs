using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.EventSystems;

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
    public Animator animator;
    private float jumpSpeed = 0.0F;
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
    private bool flipped = false;
    private bool cameraFlipped = false;
    private EventSystem eventSystem;
    private bool playerMoving = true;

	void Start () {
		// isMobile = (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer);
        isMobile = true;
        Debug.Log("isMobile = " + isMobile);
		controller = GetComponent<CharacterController>();
		leftMinDragDistance = 5;
		rightMinDragDistance = 20;
        eventSystem = EventSystem.current;
		touchPositionsDict = new TouchPositionsDict(eventSystem);
		rightFingerAction = FingerAction.off;
	}

	void FixedUpdate() {
		if (isMobile) {
			if (controller.isGrounded) {
				moveDirection = new Vector3(leftFingerHorizontal, 0, leftFingerVertical);
				moveDirection *= speed;
				Jump();
				moveDirection = transform.TransformDirection(moveDirection);
			}
			moveDirection.y -= gravity * Time.deltaTime;
			controller.Move(moveDirection * Time.deltaTime);

            if (leftFingerHorizontal > 0 && !flipped) {
                FlipSprite();
            } else if (leftFingerHorizontal < 0 && flipped) {
                FlipSprite();
            }
		}
	}

    void Jump() {
        if (rightFingerAction == FingerAction.shortTap) {
            rightFingerAction = FingerAction.off;
            moveDirection.y = jumpSpeed;
            Debug.Log("jumped");
        }
    }

    void Update() {
		if (isMobile) {
            if (playerMoving) {
                ReadTouches();
            }
		} else {
			var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
			var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;
			transform.Rotate(0, x, 0);
			transform.Translate(0, 0, z);
		}
        animator.SetFloat("speed", controller.velocity.magnitude);
	}

	void FlipSprite() {
		if (flipped) {
			animator.transform.localRotation = Quaternion.Euler(0, 0, 0);
		} else {
			animator.transform.localRotation = Quaternion.Euler(0, 180, 0);
		}
		flipped = !flipped;
    }

    public void FlipCamera() {
		cameraFlipped = !cameraFlipped;
		int yRotation = 0;
		if (cameraFlipped) {
			yRotation = 180;
		}
		transform.localRotation = Quaternion.Euler(0, yRotation, 0);
		EventManager.TriggerEvent(Constants.EVENT_PLAYER_FLIPPED, FlippedCameraMessage.CreateHashtable(cameraFlipped));
	}

	void ReadTouches() {
		foreach (Touch touch in Input.touches) {
			if (touch.phase == TouchPhase.Began) {
                touchPositionsDict.AddFingerPosition(
                    touch.fingerId,
                    touch.position,
                    Time.time,
                    !eventSystem.IsPointerOverGameObject(touch.fingerId)
                );
            }
            else if (touch.phase == TouchPhase.Moved) {
                CheckMovementJoysticks(touch);
            }
            else if (touch.phase == TouchPhase.Ended) {
				float minDistance;
				bool isLeft = touchPositionsDict.GetOnLeftForId(touch);
				if (isLeft) {
					leftFingerHorizontal = 0;
                	leftFingerVertical = 0;
					minDistance = leftMinDragDistance;
				} else {
					rightFingerHorizontal = 0;
                	rightFingerVertical = 0;
					minDistance = rightMinDragDistance;
				}
                Vector3 firstPos = touchPositionsDict.GetFirstForId(touch);
                Vector3 lastPos = touch.position;
                float startTime = touchPositionsDict.GetStartTimeForId(touch);
				
                if (!touchPositionsDict.IsOnButtonForId(touch)) {
                    if (IsSwipeDistanceLargeEnough(firstPos, lastPos, minDistance)) {
                        CheckTypeOfSwipe(firstPos, lastPos, touch.fingerId);
                    } else {
                        CheckTypeOfTap(startTime);
                    }
                }
            }
		}
	}

	void CheckMovementJoysticks(Touch touch) {
        Vector3 firstPos = touchPositionsDict.GetFirstForId(touch);
        Vector3 lastPos = touch.position;
		float minDistance = touchPositionsDict.GetOnLeftForId(touch) ? leftMinDragDistance : rightMinDragDistance;
        if (IsSwipeDistanceLargeEnough(firstPos, lastPos, minDistance)) {
            Vector2 joystickVec;
            if (touchPositionsDict.GetOnLeftForId(touch)) {
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
