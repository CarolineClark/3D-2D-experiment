public class Context {
    public BakerContext baker {get; private set;}
    public MelodyContext melody {get; private set;}

    public Context() {
        baker = new BakerContext();
        melody = new MelodyContext();
        
        LoadStateFromFile();
    }

    void LoadStateFromFile() {
        // TODO
    }

    void SaveStateToFile() {

    }
}