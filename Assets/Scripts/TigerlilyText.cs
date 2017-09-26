using Mgl;

public class TigerlilyText {
    private static I18n i18n = I18n.Instance;
    static string text1 = "...";
    static string text2 = "Excuse me. I don't talk to commoners.";
    static string choice1 = "Melody says hi";
    static string reply1 = "...oh!";
    static string reply2 = "You know her?";
    static string choice2 = "(Say nothing)";

    public static StoryTree TigerlilyFirstText() {
        StoryTree tree = new StoryTree();
        InsertTextIntoTree(tree, text1, text2);
        return tree;
    }

    public static StoryTree TigerlilyFirstTextWithChoices() {
        StoryTree tree = new StoryTree();
        InsertTextIntoTree(tree, text1, text2);
        StoryTree choice1tree = CreateChoiceAndReplies(choice1, reply1, reply2);
        StoryTree choice2tree = CreateChoiceAndReplies(choice2);
        AddChoicesToTree(tree, choice1tree, choice2tree);
        return tree;
    }

    private static void InsertTextIntoTree(StoryTree tree, params string[] list) {
        tree.text = new string[list.Length];
        for (int i = 0; i < list.Length; i++)
        {
            tree.text[i] = i18n.__(list[i]);
        }
    }

    private static StoryTree CreateChoiceAndReplies(string choice, params string[] list) {
        StoryTree choicetree = new StoryTree();
        choicetree.selected = i18n.__(choice);
        InsertTextIntoTree(choicetree, list);
        return choicetree;
    }

    private static void AddChoicesToTree(StoryTree tree, params StoryTree[] list) {
        tree.choices = list;
    }
}