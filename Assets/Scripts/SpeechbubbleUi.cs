using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class SpeechbubbleUi : IConversationUiElement {
    RectTransform RectTransformUi;
    GameObject SpeechGameObject;
    Canvas canvas;
    Image ImageUi;
	public Text TextUi { get; }
    public Button ButtonUi { get; }
    private int fontSize = 100;
	private float typingSpeed = .03f;
    private string textToShow;
	public IConversationUiElement NextElement { get; set; }
    private MonoBehaviour monoBehaviour;

    public SpeechbubbleUi(MonoBehaviour monoBehaviour, Canvas canvas, string textToShow, Action<IConversationUiElement> cb) {
        this.monoBehaviour = monoBehaviour;
        this.textToShow = textToShow;
		
        Sprite speakingBubbleImage = Resources.Load<Sprite>("speaking-speechbubble@2x");

		SpeechGameObject = new GameObject();
		SpeechGameObject.transform.SetParent(canvas.transform);
		SpeechGameObject.SetActive(false);

		RectTransformUi = SpeechGameObject.AddComponent<RectTransform>();
		RectTransformUi.anchorMin = new Vector2(0.0f, 0);
		RectTransformUi.anchorMax = new Vector2(1, 1);
		RectTransformUi.offsetMin = new Vector2(0, 0);
		RectTransformUi.offsetMax = new Vector2(0, 0);
		RectTransformUi.pivot = new Vector2(0.5f, 0);

		GameObject imageGameobject = new GameObject();
		ButtonUi = imageGameobject.AddComponent<Button>();
		ButtonUi.enabled = false;
        ButtonUi.onClick.AddListener(delegate() {cb(this);});

		ImageUi = imageGameobject.AddComponent<Image>();
		imageGameobject.transform.SetParent(SpeechGameObject.transform);
		ImageUi.sprite = speakingBubbleImage;
		ImageUi.rectTransform.anchorMin = new Vector2(0, 0);
		ImageUi.rectTransform.anchorMax = new Vector2(1, 0.3f);
		ImageUi.rectTransform.offsetMin = new Vector2(30, 30);
		ImageUi.rectTransform.offsetMax = new Vector2(-30, -30);

		GameObject textGameobject = new GameObject();
		TextUi = textGameobject.AddComponent<Text>();
		textGameobject.transform.SetParent(imageGameobject.transform);
		TextUi.font = (Font)Resources.Load("Chisel Mark");
		TextUi.fontSize = fontSize;
		TextUi.color = Color.white;
		TextUi.fontStyle = FontStyle.Normal;
		TextUi.rectTransform.anchorMin = new Vector2(0, 0);
		TextUi.rectTransform.anchorMax = new Vector2(1, 1);
		TextUi.rectTransform.offsetMin = new Vector2(100, 30);
		TextUi.rectTransform.offsetMax = new Vector2(-30, -30);
		TextUi.alignment = TextAnchor.MiddleLeft;
	}

    public IEnumerator AnimateText(){
		TextUi.text = textToShow;
		for (int i = 0; i < (textToShow.Length + 1); i++) {
			TextUi.text = textToShow.Substring(0, i);
			yield return new WaitForSeconds(typingSpeed);
		}
		ButtonUi.enabled = true;
	}

    public void Show() {
        SpeechGameObject.SetActive(true);
        monoBehaviour.StartCoroutine(AnimateText());
    }
    public void Remove() {
        UnityEngine.Object.Destroy(SpeechGameObject);
    }
    public IConversationUiElement ShowNext() {
        if (NextElement != null) {
            NextElement.Show();
        }
		Remove();
        return NextElement;
    }
}