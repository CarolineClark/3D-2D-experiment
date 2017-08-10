using System.Collections;
using UnityEngine;

public class Message {

    public static T GetValueFromHashtable<T>(Hashtable h, string key) {
        if (HashtableContainsKey(h, key)) {
            return (T) h[key];
        }
        return default(T);
    }

    private static bool HashtableContainsKey(Hashtable h, string key) {
        return h != null && h.ContainsKey(key);
    }

} 