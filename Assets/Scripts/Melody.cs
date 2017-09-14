using System.Collections;
using System.Collections.Generic;
using Mgl;
using UnityEngine;


class Melody : INPCStory {
    public INPCConversation StartConversation() {
        string jsonTree = @"{
            ""text"": [""Say hi to Tigerlily from me!""]
        }";
        return new ConversationHelper(jsonTree);
    }
}