using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowNPCText : MonoBehaviour {

	public string key;
	Button button;
	Image image;
	Text text;

	void Start () {
		Canvas canvas = GetComponentInChildren<Canvas>();
		button = canvas.GetComponentInChildren<Button>();
		image = canvas.GetComponentInChildren<Image>();
		text = image.GetComponentInChildren<Text>();
		image.gameObject.SetActive(false);
		button.gameObject.SetActive(false);
		text.gameObject.SetActive(false);
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == Constants.PLAYER_TAG) {
			button.gameObject.SetActive(true);
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == Constants.PLAYER_TAG) {
			button.gameObject.SetActive(false);
			text.gameObject.SetActive(false);
			image.gameObject.SetActive(false);
		}
	}
	
	public void OnClickedShowSpeech () {
		Debug.Log("clicked on button");
		button.gameObject.SetActive(false);
		image.gameObject.SetActive(true);
		text.gameObject.SetActive(true);
		text.text = StoryManager.instance.GetSpeech(key);
	}
}
