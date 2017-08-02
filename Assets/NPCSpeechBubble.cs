using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCSpeechBubble : MonoBehaviour {

	Image image;
	Text text;
	GameObject responsesGameObject;
	GameObject speechGameObject;
	public Sprite thoughtBubbleImage;
	public Sprite speakingBubbleImage;
	private Canvas canvas;
	private int fontSize = 70;

	// Use this for initialization
	void Start () {
		canvas = GetComponent<Canvas>();

		EventManager.StartListening(Constants.EVENT_NPC_SPEAK, ShowMessage);
		EventManager.StartListening(Constants.EVENT_NPC_STOP_SPEAK, HideMessage);
	}
	
	private void ShowMessage (Hashtable h) {
		string dialog = NPCCharacterDialog.GetDialog(h);
		CreateSpeechBubbleWithText(dialog);
		ShowResponses(h);
	}

	private void ShowResponses(Hashtable h) {
		string key = NPCCharacterDialog.GetKeyFromHashtable(h);
		List<string> responses = NPCCharacterDialog.GetPlayerResponses(h);
		if (responses != null) {
			responsesGameObject = new GameObject();
			responsesGameObject.transform.SetParent(canvas.transform);
			RectTransform rectTransform = responsesGameObject.AddComponent<RectTransform>();
			rectTransform.anchorMin = new Vector2(0, 0);
			rectTransform.anchorMax = new Vector2(0.5f, 1);
			rectTransform.offsetMin = new Vector2(0, 0);
			rectTransform.offsetMax = new Vector2(0, 0);
			rectTransform.pivot = new Vector2(0.5f, 0);
			
			int i = 0;
			foreach (string response in responses) {
				GameObject imageGameobject = new GameObject();
				Button button = imageGameobject.AddComponent<Button>();
				button.onClick.AddListener(delegate() { ResponseClicked(key, response); });
				Image image = imageGameobject.AddComponent<Image>();
				imageGameobject.transform.SetParent(responsesGameObject.transform);
				image.sprite = thoughtBubbleImage;
				image.rectTransform.anchorMin = new Vector2(0, 0.3f * i);
				image.rectTransform.anchorMax = new Vector2(1, 0.3f * (i + 1));
				image.rectTransform.offsetMin = new Vector2(30, 30);
				image.rectTransform.offsetMax = new Vector2(-30, -30);

				GameObject textGameobject = new GameObject();
				Text text = textGameobject.AddComponent<Text>();
				textGameobject.transform.SetParent(imageGameobject.transform);
				text.text = response;
				text.font = (Font)Resources.Load("Chisel Mark");
				text.fontSize = fontSize;
				text.color = Color.black;
				text.fontStyle = FontStyle.Normal;
				text.rectTransform.anchorMin = new Vector2(0, 0);
				text.rectTransform.anchorMax = new Vector2(1, 1);
				text.rectTransform.offsetMin = new Vector2(30, 30);
				text.rectTransform.offsetMax = new Vector2(-30, -30);
				i++;

			}
		}
	}

	void ResponseClicked(string key, string response) {
		Debug.Log("response clicked = " + response);
		// need to pass npc key and response.
		EventManager.TriggerEvent(Constants.EVENT_PLAYER_RESPONDS_TO_NPC);
		HideMessage(null);
	}

	private GameObject CreateSpeechBubbleWithText(string speech) {
		speechGameObject = new GameObject();
		speechGameObject.transform.SetParent(canvas.transform);
		RectTransform rectTransform = speechGameObject.AddComponent<RectTransform>();
		rectTransform.anchorMin = new Vector2(0.5f, 0);
		rectTransform.anchorMax = new Vector2(1, 1);
		rectTransform.offsetMin = new Vector2(0, 0);
		rectTransform.offsetMax = new Vector2(0, 0);
		rectTransform.pivot = new Vector2(0.5f, 0);

		GameObject imageGameobject = new GameObject();
		Image image = imageGameobject.AddComponent<Image>();
		imageGameobject.transform.SetParent(speechGameObject.transform);
		image.sprite = speakingBubbleImage;
		image.rectTransform.anchorMin = new Vector2(0, 0);
		image.rectTransform.anchorMax = new Vector2(1, 0.3f);
		image.rectTransform.offsetMin = new Vector2(30, 30);
		image.rectTransform.offsetMax = new Vector2(-30, -30);

		GameObject textGameobject = new GameObject();
		Text text = textGameobject.AddComponent<Text>();
		textGameobject.transform.SetParent(imageGameobject.transform);
		text.text = speech;
		text.font = (Font)Resources.Load("Chisel Mark");
		text.fontSize = fontSize;
		text.color = Color.white;
		text.fontStyle = FontStyle.Normal;
		text.rectTransform.anchorMin = new Vector2(0, 0);
		text.rectTransform.anchorMax = new Vector2(1, 1);
		text.rectTransform.offsetMin = new Vector2(30, 30);
		text.rectTransform.offsetMax = new Vector2(-30, -30);
		
		return speechGameObject;
	}

	private void HideMessage (Hashtable h) {
		Destroy(responsesGameObject);
		Destroy(speechGameObject);
	}
}
