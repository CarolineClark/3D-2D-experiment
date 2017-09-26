using System;


[Serializable]
public class StoryTree {
    public string selected;
    public string[] text;
    public StoryTree[] choices;
    public Action action {set; get;}
}