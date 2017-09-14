using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCharacterDialog {

    public static void SpeakToNpc(string key) {
        Hashtable h = CreateHashtableNpc(key);
        EventManager.TriggerEvent(Constants.EVENT_NPC_SPEAK, h);
    }

    public static void StopSpeakingToNpc() {
        EventManager.TriggerEvent(Constants.EVENT_NPC_STOP_SPEAK);
    }

    public static void RespondToNpc(string key, string response) {
        Hashtable h = CreateResponseHashtableToNpc(key, response);
		EventManager.TriggerEvent(Constants.EVENT_PLAYER_RESPONDS_TO_NPC, h);
	}

    private static Hashtable CreateHashtableNpc(string key) {
        Hashtable h = new Hashtable();
        h.Add(Constants.KEY_NPC, key);
        return h;
    }

    private static Hashtable CreateResponseHashtableToNpc(string npc, string response) {
        Hashtable h = new Hashtable();
        h.Add(Constants.KEY_NPC, npc);
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
        if (h != null && h.ContainsKey(Constants.KEY_PLAYER_RESPONSE)) {
            return (string) h[Constants.KEY_PLAYER_RESPONSE];
        }
        return null;
    }

} 