using System.Collections;
using System.Collections.Generic;
using Mgl;


class Baker: INPCStory {
    private bool responded = false;
    private I18n i18n = I18n.Instance;

    public string GetSpeech() {
        if (responded) {
            return i18n.__("You talk nonsense. Go away.");
        }
        return i18n.__("What do you want?");
    }

    public void SetPlayerResponse(string response) {
        responded = true;
    }

    public List<string> GetPlayerResponses() {
        if (!responded) {
            List<string> responses = new List<string>();
            responses.Add(i18n.__("response 1"));
            responses.Add(i18n.__("response 2"));
            return responses;
        }
        return null;
    }

    public void LoadFromSave(string file) {

    }
}