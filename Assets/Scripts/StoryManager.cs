using UnityEngine;
using System.Collections;

using System.Collections.Generic;
    
public class StoryManager : MonoBehaviour {

    public static StoryManager instance = null;

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
        // load level and state
    }

    public string GetSpeech(string key) {
        if (key == Constants.BAKER_GAMEOBJECT_NAME) {
            return GetBakerSpeech();
        }
        return "";
    }

    private string GetBakerSpeech() {
        return "What do you want?";
    }


}