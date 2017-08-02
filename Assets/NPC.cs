using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour {

	public string key;
	Button button;

	void Start () {
		Canvas canvas = GetComponentInChildren<Canvas>();
		button = canvas.GetComponentInChildren<Button>();
		button.gameObject.SetActive(false);
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == Constants.PLAYER_TAG) {
			button.gameObject.SetActive(true);
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == Constants.PLAYER_TAG) {
			button.gameObject.SetActive(false);
			EventManager.TriggerEvent(Constants.EVENT_NPC_STOP_SPEAK);
		}
	}
	
	public void OnClickedShowSpeech () {
		Debug.Log("clicked on button");
		EventManager.TriggerEvent(Constants.EVENT_NPC_SPEAK, NPCCharacterDialog.CreateHashtable(key));
		button.gameObject.SetActive(false);
	}
}
