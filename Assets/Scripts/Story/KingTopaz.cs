using System.Collections;
using System.Collections.Generic;
using Mgl;
using UnityEngine;


class KingTopaz : INPCStory {
    Context context;
    
    public KingTopaz(Context context) {
        this.context = context;
    }

    public INPCConversation StartConversation() {
        string jsonTree = @"{
            ""text"": [""...."", ""Excuse me. I don't talk to commoners.""]
        }";
        return new ConversationHelper(jsonTree);
    }
}