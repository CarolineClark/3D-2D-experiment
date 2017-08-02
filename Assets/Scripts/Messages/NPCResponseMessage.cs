using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCResponseMessage {

    public static Hashtable CreateHashtable(string key, string response) {
        Hashtable h = new Hashtable();
        h.Add(Constants.KEY_NPC, key);
        h.Add(Constants.KEY_PLAYER_RESPONSE, response);
        return h;
    }

    public static string GetKeyFromHashtable(Hashtable h) {
        if (h != null && h.ContainsKey(Constants.KEY_NPC)) {
            return (string) h[Constants.KEY_NPC];
        }
        return null;
    }

    public static string GetResponseFromHashtable(Hashtable h) {
		string key = GetKeyFromHashtable(h);
        if (key != null) {
            return StoryManager.instance.GetNPCSpeech(key);
        }
        return null;
    }

    public static List<string> GetResponsesFromHashtable(Hashtable h) {
		string key = GetKeyFromHashtable(h);
        if (key != null) {
            return StoryManager.instance.GetPlayerResponses(key);
        }
        return null;
    }

} 