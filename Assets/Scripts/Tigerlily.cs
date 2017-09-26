using System.Collections;
using System.Collections.Generic;
using Mgl;
using UnityEngine;
using System.IO; 


class Tigerlily : INPCStory {
    
    TigerlilyContextHandler contextHandler;

    public Tigerlily(Context context) {
        this.contextHandler = new TigerlilyContextHandler(context);
    }

    public INPCConversation StartConversation() {
        StoryTree tree;

        if (contextHandler.TalkedToMelody()) {
            tree = TigerlilyText.TigerlilyFirstTextWithChoices();
        } else {
            tree = TigerlilyText.TigerlilyFirstText();
        }
        
        return new ConversationHelper(tree);
    }
}