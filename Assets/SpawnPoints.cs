using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour {

	private const string CASTLE = "Castle";
	private const string BAKERY = "Bakery";
	Transform castleSpawnPoint;
	Transform bakerySpawnPoint;
	public static SpawnPoints instance = null;

	void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
	}

	void Start () {
		castleSpawnPoint = transform.Find(CASTLE);
		if (transformActive(castleSpawnPoint)) {
			Debug.LogError("castle spawn point is not loaded");
			UnityEditor.EditorApplication.isPlaying = false;
		}
		bakerySpawnPoint = transform.Find(BAKERY);
		if (transformActive(bakerySpawnPoint)) {
			Debug.LogError("bakery spawn point is not loaded");
			UnityEditor.EditorApplication.isPlaying = false;
		}
	}

	private bool transformActive(Transform t) {
		return (t == null || !t.gameObject.activeSelf);
	}
	
	void Update () {
		
	}
}
