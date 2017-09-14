using System.Collections;
using System.Collections.Generic;


public class NPCStoryMessage {
    public string Key { get; }
    public List<string> TextToShow { get; }
    public List<string> Choices { get; }

    public NPCStoryMessage(string key, List<string> textToShow, List<string> choices) {
        Key = key;
        TextToShow = textToShow;
        Choices = choices;
    }
    
    public IEnumerator<string> GetIterator() {
        foreach (string text in TextToShow) {
            yield return text;
        }
    }
}