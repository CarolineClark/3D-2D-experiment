using System.Collections;
using System.Collections.Generic;

public interface INPCStory {
    string GetSpeech();
    List<string> GetPlayerResponses();
    void SetPlayerResponse(string response);
    void LoadFromSave(string file);
}