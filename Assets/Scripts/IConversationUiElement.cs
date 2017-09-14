public interface IConversationUiElement {
    void Show();
    void Remove();
    IConversationUiElement ShowNext();
    IConversationUiElement NextElement { get; set; }
}