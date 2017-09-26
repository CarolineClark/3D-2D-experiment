using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SpeechBubbleViewModel : MonoBehaviour {
	private Canvas canvas;
	private IConversationUiElement currentElement;
    private INPCConversation conversation;

	void Start () {
		canvas = GetComponent<Canvas>();

		EventManager.StartListening(Constants.EVENT_NPC_SPEAK, StartConversationEventListener);
		EventManager.StartListening(Constants.EVENT_NPC_STOP_SPEAK, HideMessage);
	}

	private void StartConversationEventListener(Hashtable h) {
		string npcKey = NPCCharacterDialog.GetKeyFromHashtable(h);
		conversation = StoryManager.instance.GetNPCSpeech(npcKey).StartConversation();
		InitialiseUIElements(conversation);
	}

	private void InitialiseUIElements(INPCConversation conversation) {
		List<IConversationUiElement> uiElements = new List<IConversationUiElement>();
		NPCStoryMessage npcStoryMessage = conversation.GetStory();
		foreach (string text in npcStoryMessage.TextToShow) {
            Action<IConversationUiElement> cb = delegate(IConversationUiElement button) { ShowNextElement(button); };
            SpeechbubbleUi speechBubble = new SpeechbubbleUi(this, canvas, text, cb);
			uiElements.Add(speechBubble);
		}
		List<string> choices = npcStoryMessage.Choices;
		if (choices != null && choices.Count > 0) {
			Action<string, IConversationUiElement> cb = delegate(string text, IConversationUiElement element) { OnResponseClicked(text, element); };
			ChoicesUi choicesUi = new ChoicesUi(canvas, choices, cb);
			uiElements.Add(choicesUi);
		}
		
		for (int i=0; i<=uiElements.Count - 2; i++) {
			uiElements[i].NextElement = uiElements[i + 1];
		}
		
		if (uiElements.Count > 0) {
			currentElement = uiElements[0];
			currentElement.Show();
		} else {
			EventManager.TriggerEvent(Constants.EVENT_NPC_STOP_SPEAK, null);
		}
	}

    void OnResponseClicked(string text, IConversationUiElement element) {
        conversation.SetSelectedChoice(text);
		InitialiseUIElements(conversation);
		element.Remove();
    }

	void ShowNextElement(IConversationUiElement element) {
		currentElement = element.ShowNext();
		if (currentElement == null) {
			EventManager.TriggerEvent(Constants.EVENT_NPC_STOP_SPEAK, null);
		}
	}

	void HideMessage(Hashtable h) {
		if (currentElement != null) {
			currentElement.Remove();
		}
	}
}
