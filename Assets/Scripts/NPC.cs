using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class NPC : MonoBehaviour {

	public string key;
	Button button;

	void Start () {
		Canvas canvas = GetComponentInChildren<Canvas>();
		button = canvas.GetComponentInChildren<Button>();
		button.gameObject.SetActive(false);
		UnityAction cb = delegate() {OnClickedShowSpeech();};
		button.onClick.AddListener(cb);
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == Constants.PLAYER_TAG) {
			button.gameObject.SetActive(true);
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == Constants.PLAYER_TAG) {
			button.gameObject.SetActive(false);
			NPCCharacterDialog.StopSpeakingToNpc();
		}
	}
	
	public void OnClickedShowSpeech () {
		NPCCharacterDialog.SpeakToNpc(key);
		button.gameObject.SetActive(false);
		EventManager.StartListening(Constants.EVENT_NPC_STOP_SPEAK, ShowButton);
	}

	void ShowButton(Hashtable h) {
		button.gameObject.SetActive(true);
		EventManager.StopListening(Constants.EVENT_NPC_STOP_SPEAK, ShowButton);
	}
}
