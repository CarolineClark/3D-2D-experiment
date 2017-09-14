public interface INPCConversation {
    NPCStoryMessage GetStory();
    void SetSelectedChoice(string response);
}