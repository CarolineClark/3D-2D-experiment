using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour {

	private const string CASTLE = "Main/Castle";
	private const string BAKERY_IN_MAIN_SCENE = "Main/Bakery";
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
		Debug.Log("Spawnpoint instance = " + instance);
		castleSpawnPoint = transform.Find(CASTLE);
		if (transformActive(castleSpawnPoint)) {
			Debug.LogError("castle spawn point is not loaded");
			UnityEditor.EditorApplication.isPlaying = false;
		}
		bakerySpawnPoint = transform.Find(BAKERY_IN_MAIN_SCENE);
		if (transformActive(bakerySpawnPoint)) {
			Debug.LogError("bakery spawn point is not loaded");
			UnityEditor.EditorApplication.isPlaying = false;
		}
	}

	public Transform CastleSpawnPoint() {
		return castleSpawnPoint;
	}

	public Transform BakerySpawnPoint() {
		return bakerySpawnPoint;
	}

	private bool transformActive(Transform t) {
		return (t == null || !t.gameObject.activeSelf);
	}
	
	void Update () {
		
	}
}
