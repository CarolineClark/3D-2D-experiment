using System.Collections;
using System.Collections.Generic;
using Mgl;
using UnityEngine;

class Baker : INPCStory {
    private I18n i18n = I18n.Instance;
    BakerContext context;

    public Baker(Context context) {
        this.context = context.baker;
    }

    public INPCConversation StartConversation() {
        string jsonTree = @"{
            ""text"": [""..."", ""What do you want?""],
            ""choices"": [
                {
                    ""selected"": ""Bread"",
                    ""text"": [""You can't afford it.""]
                },
                {
                    ""selected"": ""Nice apron"",
                    ""text"": [""My wife made it.""]
                }
            ]
        }";
        return new ConversationHelper(jsonTree);
    }
}