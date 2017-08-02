using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCharacterDialog {

    public static Hashtable CreateHashtable(string key) {
        Hashtable h = new Hashtable();
        h.Add(Constants.KEY_NPC, key);
        return h;
    }

    public static string GetKeyFromHashtable(Hashtable h) {
        if (h != null && h.ContainsKey(Constants.KEY_NPC)) {
            return (string) h[Constants.KEY_NPC];
        }
        return null;
    }

    public static string GetDialog(Hashtable h) {
		string key = GetKeyFromHashtable(h);
        if (key != null) {
            return StoryManager.instance.GetNPCSpeech(key);
        }
        return null;
    }

    public static List<string> GetPlayerResponses(Hashtable h) {
		string key = GetKeyFromHashtable(h);
        if (key != null) {
            return StoryManager.instance.GetPlayerResponses(key);
        }
        return null;
    }

} 