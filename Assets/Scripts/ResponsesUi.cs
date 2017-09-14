using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class ChoicesUi : IConversationUiElement {
    GameObject responsesGameObject;
    private int fontSize = 100;
    Renderer renderer;
    public IConversationUiElement NextElement { get; set; }

    public ChoicesUi(Canvas canvas, List<string> responses, Action<string, IConversationUiElement> action) {
		if (responses != null) {
            Sprite thoughtBubbleImage = Resources.Load<Sprite>("thought-bubble@2x");

			responsesGameObject = new GameObject();
			responsesGameObject.SetActive(false);
			responsesGameObject.transform.SetParent(canvas.transform);

			RectTransform rectTransform = responsesGameObject.AddComponent<RectTransform>();
			rectTransform.anchorMin = new Vector2(0, 0);
			rectTransform.anchorMax = new Vector2(1, 1);
			rectTransform.offsetMin = new Vector2(0, 0);
			rectTransform.offsetMax = new Vector2(0, 0);
			rectTransform.pivot = new Vector2(0.5f, 0);
			
			int i = 0;
			foreach (string response in responses) {
				GameObject imageGameobject = new GameObject();
				Button button = imageGameobject.AddComponent<Button>();
				button.onClick.AddListener(delegate() { action(response, this); });
				Image image = imageGameobject.AddComponent<Image>();
				imageGameobject.transform.SetParent(responsesGameObject.transform);
				image.sprite = thoughtBubbleImage;
				image.rectTransform.anchorMin = new Vector2(0.5f * i, 0.0f);
				image.rectTransform.anchorMax = new Vector2(0.5f * (i + 1), 0.3f);
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
				text.rectTransform.offsetMin = new Vector2(50, 30);
				text.rectTransform.offsetMax = new Vector2(-50, -30);
				text.alignment = TextAnchor.MiddleLeft;
				i++;
			}
		}
	}

    public void Show() {
        responsesGameObject.SetActive(true);
    }
    public void Remove() {
        UnityEngine.Object.Destroy(responsesGameObject);
    }
    public IConversationUiElement ShowNext() {
        Remove();
        if (NextElement != null) {
            NextElement.Show();
        }
        return NextElement;
    }
}