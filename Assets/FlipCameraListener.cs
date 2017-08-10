using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipCameraListener : MonoBehaviour {
	private bool flipped = false;

	public void FlipPlayer() {
		flipped = !flipped;
		int yRotation = 0;
		if (flipped) {
			yRotation = 180;
		}
		transform.localRotation = Quaternion.Euler(0, yRotation, 0);
		EventManager.TriggerEvent(Constants.EVENT_PLAYER_FLIPPED, FlippedCameraMessage.CreateHashtable(flipped));
	}
}
