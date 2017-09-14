using System.Collections;
using System.Collections.Generic;

public class NPCStoryMessage {
    public List<string> TextToShow { get; }
    public List<string> Choices { get; }

    public NPCStoryMessage(List<string> textToShow, List<string> choices) {
        TextToShow = textToShow;
        Choices = choices;
    }
    
    public IEnumerator<string> GetIterator() {
        foreach (string text in TextToShow) {
            yield return text;
        }
    }
}