using UnityEngine;

class TigerlilyContextHandler {
    Context context;
    public TigerlilyContextHandler(Context context) {
        this.context = context;
    }
    public bool TalkedToMelody() {
        return this.context.melody.spokeOutsideCastle;
    }

    public bool MentionedMelody() {
        return this.context.tigerlily.MentionedMelody;
    }
}