using UnityEngine;

public class StoryManager : MonoBehaviour {
	private static StoryManager storyManager;
	public static StoryManager instance  
    {
        get
        {
            if (!storyManager)
            {
				storyManager = FindObjectOfType (typeof (StoryManager)) as StoryManager;
                if (!storyManager)
                {
                    Debug.LogError("Need an active StoryManager on a GameObject in your scene");
                }
                else 
                {
                    storyManager.Init();
                }
            }
            return storyManager;
        }
    }

	void Init() 
    {
        // setup story
    }

    public static string TalkToNPC(string NPCId) {
        return "stuff";
    }
}