using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	GameObject playerCamera;
	private float lerpSpeed = 5.0F;
    private float startTime;
	private Vector3 startPosition;
    private Vector3 endPosition;
	private Quaternion startRotation;
    private Quaternion endRotation;
	private float journeyLength;
	private bool controlledByPlayer = true;
	IEnumerator movingCameraCoroutine;
	private bool cameraFlipped = false;
	private Vector3 playerCameraOffset = new Vector3(0, 4.3f, -11.21f); // TODO this should be changed depending on the area.


	void Start () {
		playerCamera = GameObject.FindGameObjectWithTag(Constants.TAG_MAIN_CAMERA);
	}
	
	void Update () {
		if (controlledByPlayer) {
			FollowPlayer();
			SetCameraAngle();
		}
	}

	void OnTriggerEnter(Collider other) {
		controlledByPlayer = false;
		if (other.tag == Constants.TAG_CAMERA_FIXED_POINT) {
			startTime = Time.time;
			startPosition = playerCamera.transform.position;
			endPosition = other.transform.position;
			startRotation = playerCamera.transform.rotation;
			endRotation = other.transform.GetChild(0).rotation;
			journeyLength = Vector3.Distance(startPosition, endPosition);
			movingCameraCoroutine = MoveCamera();
			StartCoroutine(movingCameraCoroutine);
		}
	}

	IEnumerator MoveCamera() {
		while (transform.position != endPosition) {
			float distCovered = (Time.time - startTime) * lerpSpeed;
			float fracJourney = distCovered / journeyLength;	
			playerCamera.transform.position = Vector3.Lerp(startPosition, endPosition, fracJourney);
			playerCamera.transform.rotation = Quaternion.Lerp(startRotation, endRotation, fracJourney);
			yield return null;
		}	
	}

	void OnTriggerExit(Collider other) {
		StopCoroutine(movingCameraCoroutine);
		controlledByPlayer = true; // TODO: lerp back to player
	}

	void FollowPlayer() {
		playerCamera.transform.position = transform.position + playerCameraOffset;
	}

	void SetCameraAngle() {
		playerCamera.transform.rotation = startRotation;
	}
	public void FlipCamera() {
		cameraFlipped = !cameraFlipped;
		int yRotation = 0;
		if (cameraFlipped) {
			yRotation = 180;
		}
		playerCamera.transform.localRotation = Quaternion.Euler(0, yRotation, 0);
		startRotation.y = yRotation;
		playerCameraOffset.x *= -1;
		playerCameraOffset.z *= -1;
	}
}
