using Mgl;
using System;

public class StoryTreeUtils {
    private static I18n i18n = I18n.Instance;

    public static void InsertTextIntoTree(StoryTree tree, params string[] list) {
        tree.text = new string[list.Length];
        for (int i = 0; i < list.Length; i++)
        {
            tree.text[i] = i18n.__(list[i]);
        }
    }

    public static StoryTree CreateChoiceAndReplies(string choice, Action action, params string[] list) {
        StoryTree choicetree = new StoryTree();
        if (action == null) {
            action = delegate() {};
        }
        choicetree.action = action;
        choicetree.selected = i18n.__(choice);

        if (list == null) {
            list = new string[] {};
        }
        InsertTextIntoTree(choicetree, list);
        return choicetree;
    }

    public static StoryTree CreateChoiceAndReplies(string choice, params string[] list) {
        return CreateChoiceAndReplies(choice, delegate() {}, list);
    }

    public static void AddChoicesToTree(StoryTree tree, params StoryTree[] list) {
        tree.choices = list;
    }
}