using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using Mgl;
    
public class StoryManager : MonoBehaviour {

    public static StoryManager instance = null;
    Context context;
    Baker baker;
    Melissa melissa;
    Melody melody;
    Tigerlily tigerlily;
    KingTopaz kingTopaz;
    private I18n i18n = I18n.Instance;
    public bool canContinue { get; }

    void Awake() {
        if (instance == null) {
            Debug.Log("new instance assigned");
            instance = this;
        } else if (instance != this) {
            Debug.Log("Destroying old instance");
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        InitGame();
    }

    void InitGame() {
        I18n.SetLocale("en-GB");
        Debug.Log("hit initgame()");
        if (context == null) {
            Debug.Log("initialisaing context");
            context = new Context();
            baker = new Baker(context);
            melissa = new Melissa();
            melody = new Melody(context);
            kingTopaz = new KingTopaz(context);
            tigerlily = new Tigerlily(context);
        }
    }

    public INPCStory GetNPCSpeech(string key) {
        switch(key) {
            case Constants.BAKER_GAMEOBJECT_NAME:
                return baker;
            case Constants.MELISSA_GAMEOBJECT_NAME:
                return melissa;
            case Constants.MELODY_GAMEOBJECT_NAME:
                return melody;
            case Constants.TIGERLILY_GAMEOBJECT_NAME:
                return tigerlily;
            case Constants.TOPAZ_GAMEOBJECT_NAME:
                return kingTopaz;
        }
        return null;
    }
}