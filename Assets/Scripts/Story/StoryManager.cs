using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using Mgl;
    
public class StoryManager : MonoBehaviour {

    public static StoryManager instance = null;
    Baker baker;
    private I18n i18n = I18n.Instance;
    public bool canContinue { get; }

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

    public INPCStory GetNPCSpeech(string key) {
        switch(key) {
            case Constants.BAKER_GAMEOBJECT_NAME:
                return baker;
        }
        return null;
    }
}