public class Context {
    public BakerContext baker {get; private set;}
    public MelodyContext melody {get; private set;}
    public TigerlilyContext tigerlily {get; private set;}

    public Context() {
        LoadStateFromFile();
    }

    void LoadStateFromFile() {
        // TODO
        baker = new BakerContext();
        melody = new MelodyContext();
        tigerlily = new TigerlilyContext();
    }

    void SaveStateToFile() {

    }
}