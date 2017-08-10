using System.Collections;
using UnityEngine;

public class FlippedCameraMessage {
    private static readonly string FLIPPED_KEY = "flipped";

    public static Hashtable CreateHashtable(bool flipped) {
        Hashtable h = new Hashtable();
        h.Add(FLIPPED_KEY, flipped);
        return h;
    }

    public static bool GetFlippedFromHashtable(Hashtable h) {
		return Message.GetValueFromHashtable<bool>(h, FLIPPED_KEY);
    }
} 