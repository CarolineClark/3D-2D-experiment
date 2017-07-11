using UnityEngine;
using UnityEngine.UI;

public class NPCSpeechBubble : MonoBehaviour {
	public string key;
	public Sprite sprite;
	private Canvas speechBubble;

	void Start () {
		speechBubble = GetComponentInChildren<Canvas>();
		speechBubble.enabled = false;
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == Constants.PLAYER_TAG) {
			speechBubble.enabled = true;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == Constants.PLAYER_TAG) {
			speechBubble.enabled = false;
			StopSpeaking();
		}
	}

	public void OnSpeak() {
        EventManager.TriggerEvent(
			Constants.EVENT_NPC_SPEAK, 
			NPCDialogMessage.CreateHashtable(GetTextFromStory(), this.sprite)
		);
	}

	void StopSpeaking() {
        EventManager.TriggerEvent(Constants.EVENT_NPC_STOP_SPEAK);
	}

	string GetTextFromStory() {
		return StoryManager.TalkToNPC(this.key);
	}
}
