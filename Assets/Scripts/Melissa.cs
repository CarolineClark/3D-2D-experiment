using System.Collections;
using System.Collections.Generic;
using Mgl;
using UnityEngine;


class Melissa : INPCStory {
    public INPCConversation StartConversation() {
        string jsonTree = @"{
            ""text"": [""Hello dear!"", ""Aren't you a cutie!""],
            ""choices"": [
                {
                    ""selected"": ""Don't patronise me."",
                    ""text"": [""Oof. Well, I still think you're cute.""]
                },
                {
                    ""selected"": ""...indeed. Can I have an apple?"",
                    ""text"": [""You don't have an inventory yet.""]
                }
            ]
        }";
        return new ConversationHelper(jsonTree);
    }
}