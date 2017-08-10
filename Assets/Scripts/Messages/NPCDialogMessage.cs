using System.Collections;
using UnityEngine;

public class NPCDialogMessage {
    private static readonly string TEXT_KEY = "text";
    private static readonly string IMAGE_KEY = "image";

    public static Hashtable CreateHashtable(string text, Sprite sprite) {
        Hashtable h = new Hashtable();
        h.Add(TEXT_KEY, text);
        h.Add(IMAGE_KEY, sprite);
        return h;
    }

    public static string GetTextFromHashtable(Hashtable h) {
		return Message.GetValueFromHashtable<string>(h, TEXT_KEY);
    }

    public static Sprite GetSpriteFromHashtable(Hashtable h) {
        return Message.GetValueFromHashtable<Sprite>(h, IMAGE_KEY);
    }

} 