using System.Collections;
using UnityEngine;

public class NPCQuestionMessage {
    private static readonly string IMAGE_KEY = "image";
    private static readonly string QUESTION_TEXT_KEY = "question_text";
    private static readonly string RESPONSE_TEXT_KEY_1 = "responses_text_1";
    private static readonly string RESPONSE_TEXT_KEY_2 = "response_text_2";
    private static readonly string RESPONSE_TEXT_KEY_3 = "response_text_3";

    public static Hashtable CreateHashtable(string question, 
                                            string response1, 
                                            string response2, 
                                            string response3, 
                                            Sprite sprite) {
        Hashtable h = new Hashtable();
        h.Add(QUESTION_TEXT_KEY, question);
        h.Add(IMAGE_KEY, sprite);
        h.Add(RESPONSE_TEXT_KEY_1, response1);
        h.Add(RESPONSE_TEXT_KEY_2, response2);
        h.Add(RESPONSE_TEXT_KEY_3, response3);
        return h;
    }
    public static string GetQuestionTextFromHashtable(Hashtable h) {
		return GetStringFromHashtable(h, QUESTION_TEXT_KEY);
    }
    public static string GetResponseText1FromHashtable(Hashtable h) {
		return GetStringFromHashtable(h, RESPONSE_TEXT_KEY_1);
    }
    public static string GetResponseText2FromHashtable(Hashtable h) {
		return GetStringFromHashtable(h, RESPONSE_TEXT_KEY_2);
    }
    public static string GetResponseText3FromHashtable(Hashtable h) {
		return GetStringFromHashtable(h, RESPONSE_TEXT_KEY_3);
    }
    private static string GetStringFromHashtable(Hashtable h, string key) {
		
        if (h != null && h.ContainsKey(key)) {
            return (string) h[key];
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