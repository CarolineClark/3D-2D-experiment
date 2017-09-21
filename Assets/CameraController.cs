using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	GameObject playerCamera;
	public bool controlledByPlayerInThisScene;
	private float lerpSpeed = 7.0F;
	private float lerpTimeToPlayer = 1.0F;
	private float offsetLerpSpeed = 10.0f;
	private Quaternion cameraRotation;
	private bool controlledByPlayer = true;
	IEnumerator movingCameraCoroutine;
	IEnumerator offsetCameraCoroutine;
	private bool cameraFlipped = false;
	public Vector3 playerCameraOffset = new Vector3(0, 4.3f, -11.21f);
	private Vector3 oldCameraOffset;


	void Start () {
		controlledByPlayer = controlledByPlayerInThisScene;
		playerCamera = GameObject.FindGameObjectWithTag(Constants.TAG_MAIN_CAMERA);
		oldCameraOffset = playerCameraOffset;
	}
	
	void Update () {
		if (controlledByPlayer) {
			FollowPlayer();
			SetCameraAngle();
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == Constants.TAG_CAMERA_FIXED_POINT) {
			Transform child = other.transform.GetChild(0);
			if (other.transform.GetChild(0) == null) {
				Debug.LogError("There is no child on the camera position object!");
				return;
			}
			if (movingCameraCoroutine != null) {
				StopCoroutine(movingCameraCoroutine);
			}
			movingCameraCoroutine = MoveCamera(child);
			StartCoroutine(movingCameraCoroutine);

		} else if (other.tag == Constants.TAG_CAMERA_CHANGE_OFFSET) {
			SetCameraOffset offsetScript = other.GetComponent<SetCameraOffset>();
			if (offsetScript == null) {
				Debug.LogError("There is no SetCameraOffset component on the gameobject!");
				return;
			}
			if (offsetCameraCoroutine != null) {
				StopCoroutine(offsetCameraCoroutine);
			}
			offsetCameraCoroutine = ChangeOffset(offsetScript.cameraOffset);
			StartCoroutine(offsetCameraCoroutine);
		}
	}

	IEnumerator ChangeOffset(Vector3 endOffset) {
		Vector3 startOffset = playerCameraOffset;
		float journeyLength = (startOffset - endOffset).magnitude;
		float startTime = Time.time;
		while (playerCameraOffset != endOffset) {
			Debug.Log(playerCameraOffset);
			float distCovered = (Time.time - startTime) * offsetLerpSpeed;
			float fracJourney = distCovered / journeyLength;
			playerCameraOffset = Vector3.Lerp(startOffset, endOffset, fracJourney);
			yield return null;
		}	
	}

	IEnumerator MoveCamera(Transform child) {
		controlledByPlayer = false;
		float startTime = Time.time;
		Vector3 startPosition = playerCamera.transform.position;
		Vector3 endPosition = child.position;
		Quaternion startRotation = playerCamera.transform.rotation;
		Quaternion endRotation = child.rotation;
		float journeyLength = Vector3.Distance(startPosition, endPosition);
		float fracJourney = 0;
		while (fracJourney < 1) {
			float distCovered = (Time.time - startTime) * lerpSpeed;
			fracJourney = distCovered / journeyLength;
			playerCamera.transform.position = Vector3.Lerp(startPosition, endPosition, fracJourney);
			playerCamera.transform.rotation = Quaternion.Lerp(startRotation, endRotation, fracJourney);
			yield return null;
		}	
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == Constants.TAG_CAMERA_FIXED_POINT) {
			if (movingCameraCoroutine != null) {
				StopCoroutine(movingCameraCoroutine);
			}
			movingCameraCoroutine = MoveCameraToPlayer();
			StartCoroutine(movingCameraCoroutine);
		} else if (other.tag == Constants.TAG_CAMERA_CHANGE_OFFSET) {
			if (offsetCameraCoroutine != null) {
				StopCoroutine(offsetCameraCoroutine);
			}
			offsetCameraCoroutine = ChangeOffset(oldCameraOffset);
			StartCoroutine(offsetCameraCoroutine);
		}
	}

	IEnumerator MoveCameraToPlayer() {
		float startTime = Time.time;
		Vector3 startPosition = playerCamera.transform.position;
		float fracJourney = 0;
		Quaternion startRotation = playerCamera.transform.rotation;
		Quaternion endRotation = Quaternion.identity;
		while (fracJourney < 1) {
			Vector3 endPosition = CameraOffsetByPlayer();
			fracJourney = (Time.time - startTime) / lerpTimeToPlayer;
			playerCamera.transform.position = Vector3.Lerp(startPosition, endPosition, fracJourney);
			playerCamera.transform.rotation = Quaternion.Lerp(startRotation, endRotation, fracJourney);
			yield return null;
		}
		controlledByPlayer = true;
	}

	void FollowPlayer() {
		if (playerCamera != null) {
			playerCamera.transform.position = CameraOffsetByPlayer();
		}
	}

	Vector3 CameraOffsetByPlayer() {
		return transform.position + playerCameraOffset;
	}

	void SetCameraAngle() {
		playerCamera.transform.rotation = cameraRotation;
	}
	public void FlipCamera() {
		cameraFlipped = !cameraFlipped;
		int yRotation = 0;
		if (cameraFlipped) {
			yRotation = 180;
		}
		playerCamera.transform.localRotation = Quaternion.Euler(0, yRotation, 0);
		cameraRotation.y = yRotation;
		playerCameraOffset.x *= -1;
		playerCameraOffset.z *= -1;
	}
}
