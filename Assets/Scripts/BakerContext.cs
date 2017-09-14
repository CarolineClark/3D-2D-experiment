public class BakerContext {
    public enum WifeStatus {
        SICK, DEAD, RECOVERED
    }

    public enum PlayerKnowledge {
        NONE, JUST_DISCOVERED, KNOWS
    }

    public enum BakerLocation {
        DOWNSTAIRS, UPSTAIRS
    }

    public WifeStatus wifeStatus { get; set; }
    public PlayerKnowledge playerKnowledge { get; set; }
    public BakerLocation bakerLocation { get; set; }
}