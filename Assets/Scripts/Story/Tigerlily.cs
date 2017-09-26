using System.Collections;
using System.Collections.Generic;
using Mgl;
using UnityEngine;
using System.IO; 


class Tigerlily : INPCStory {
    
    TigerlilyContextHandler contextHandler;
    TigerlilyContext context;

    public Tigerlily(Context context) {
        this.contextHandler = new TigerlilyContextHandler(context);
        this.context = context.tigerlily;
    }

    public INPCConversation StartConversation() {
        StoryTree tree;

        if (contextHandler.TalkedToMelody()) {
            tree = TigerlilyText.TigerlilyFirstTextWithChoices();
            this.context.MentionedMelody = true;
        } else {
            tree = TigerlilyText.TigerlilyFirstText();
        }
        
        return new ConversationHelper(tree);
    }
}