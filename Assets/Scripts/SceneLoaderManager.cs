using UnityEngine;
using  UnityEngine.SceneManagement;

public class SceneLoaderManager : MonoBehaviour {
    public static SceneLoaderManager instance = null;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
	}
    
    public void LoadMainSceneAtCastle() {
        SceneManager.sceneLoaded += LoadingMainSceneFromCastle;
        LoadMainScene();
    }

    public void LoadMainSceneAtBakery() {
        SceneManager.sceneLoaded += LoadingMainSceneFromBakery;
        LoadMainScene();
    }

    private void LoadMainScene() {
        SceneManager.LoadScene(Constants.MAIN_SCENE);
    }

    void LoadingMainSceneFromCastle(Scene scene, LoadSceneMode mode)
    {
        GameObject player = GameObject.FindWithTag(Constants.PLAYER_TAG);
        player.transform.position = SpawnPoints.instance.CastleSpawnPoint().position;
        SceneManager.sceneLoaded -= LoadingMainSceneFromCastle;
    }

    void LoadingMainSceneFromBakery(Scene scene, LoadSceneMode mode)
    {
        GameObject player = GameObject.FindWithTag(Constants.PLAYER_TAG);
        player.transform.position = SpawnPoints.instance.BakerySpawnPoint().position;
        SceneManager.sceneLoaded -= LoadingMainSceneFromBakery;
    }
}