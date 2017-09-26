using System.Collections;
using System.Collections.Generic;
using Mgl;
using UnityEngine;


class Melody : INPCStory {
    MelodyContext context;
    public Melody(Context context) {
        this.context = context.melody;
    }

    public INPCConversation StartConversation() {    
        string jsonTree = @"{
            ""text"": [""Say hi to Tigerlily from me!""]
        }";
        context.spokeOutsideCastle = true;
        return new ConversationHelper(jsonTree);
    }
}