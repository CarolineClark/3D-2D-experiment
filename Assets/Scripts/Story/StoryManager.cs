using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using Mgl;
    
public class StoryManager : MonoBehaviour {

    public static StoryManager instance = null;
    Baker baker;
    private I18n i18n = I18n.Instance;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        InitGame();
    }

    void InitGame() {
        I18n.SetLocale("en-GB");
        baker = new Baker();
    }

    void Start() {
        EventManager.StartListening(Constants.EVENT_PLAYER_RESPONDS_TO_NPC, RepliedToNPC);
    }

    public string GetNPCSpeech(string key) {
        switch(key) {
            case Constants.BAKER_GAMEOBJECT_NAME:
                return baker.GetSpeech();
        }
        return "";
    }

    public List<string> GetPlayerResponses(string key) {
        switch(key) {
            case Constants.BAKER_GAMEOBJECT_NAME:
                return baker.GetPlayerResponses();
        }
        return new List<string>();
    }

    public void RepliedToNPC(Hashtable h) {
        baker.SetPlayerResponse("something");
    }

}