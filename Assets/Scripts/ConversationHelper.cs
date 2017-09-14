using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationHelper: INPCConversation {
    int counter = 0;
    StoryTree storyTree;

    public ConversationHelper(string json) {
        storyTree = JsonUtility.FromJson<StoryTree>(json);
    }

    public NPCStoryMessage GetStory() {
        List<string> choices = new List<string>();
        foreach (StoryTree choice in storyTree.choices ?? new StoryTree[]{}) {
            choices.Add(choice.selected);
        }
        counter++;

        List<string> textList = new List<string>(storyTree.text);
        return new NPCStoryMessage(textList, choices);
    }

    public void SetSelectedChoice(string selectedChoice) {
        foreach (StoryTree choice in storyTree.choices) {
            if (choice.selected == selectedChoice) {
                storyTree = choice;
            }
        }
    }
}