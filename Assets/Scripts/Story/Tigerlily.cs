using System.Collections;
using System.Collections.Generic;
using Mgl;
using UnityEngine;
using System.IO;
using System;


class Tigerlily : INPCStory {
    
    TigerlilyContextHandler contextHandler;
    TigerlilyContext context;
    readonly static string ELLIPSIS = "...";
    readonly static string DISMISSIVE = "Excuse me. I don't talk to commoners.";
    static string MELODY_SAYS_HI = "Melody says hi";
    static string OH = "...oh!";
    static string YOU_KNOW_HER = "You know her?";
    static string SAY_NOTHING = "[Say nothing]";
    static string SHHHH = "Shhhh..";
    static string DONT_TELL_FATHER = "Don't tell my father about this.";

    public Tigerlily(Context context) {
        this.contextHandler = new TigerlilyContextHandler(context);
        this.context = context.tigerlily;
    }

    public INPCConversation StartConversation() {
        StoryTree tree;

        if (contextHandler.MentionedMelody()) {
            tree = MelodyAcknowledged();
        } else if (contextHandler.TalkedToMelody()) {
            tree = TigerlilyFirstTextWithChoices();
        } else {
            tree = TigerlilyFirstText();
        }
        
        return new ConversationHelper(tree);
    }

    StoryTree TigerlilyFirstTextWithChoices() {
        StoryTree tree = new StoryTree();
        Action action  = delegate() {this.context.MentionedMelody = true;};
        StoryTreeUtils.InsertTextIntoTree(tree, ELLIPSIS, DISMISSIVE);
        StoryTree choice1tree = StoryTreeUtils.CreateChoiceAndReplies(MELODY_SAYS_HI, action, OH, YOU_KNOW_HER);
        StoryTree choice2tree = StoryTreeUtils.CreateChoiceAndReplies(SAY_NOTHING);
        StoryTreeUtils.AddChoicesToTree(tree, choice1tree, choice2tree);
        return tree;
    }

    StoryTree TigerlilyFirstText() {
        StoryTree tree = new StoryTree();
        StoryTreeUtils.InsertTextIntoTree(tree, ELLIPSIS, DISMISSIVE);
        return tree;
    }

    StoryTree MelodyAcknowledged() {
        StoryTree tree = new StoryTree();
        StoryTreeUtils.InsertTextIntoTree(tree, SHHHH, DONT_TELL_FATHER);
        return tree;
    }
}