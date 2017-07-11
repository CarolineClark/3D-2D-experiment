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
		
        if (h != null && h.ContainsKey(TEXT_KEY)) {
            return (string) h[TEXT_KEY];
        }
        return null;
    }

    public static Sprite GetSpriteFromHashtable(Hashtable h) {
		if (h != null && h.ContainsKey(IMAGE_KEY)) {
            return (Sprite) h[IMAGE_KEY];
        }
        return null;
    }

} 