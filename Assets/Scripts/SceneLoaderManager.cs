using UnityEngine;
using  UnityEngine.SceneManagement;

public class SceneLoaderManager : MonoBehaviour {
    public static SceneLoaderManager instance = null;
    GameObject player;
    GameObject camera;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
	}

    public void Start() {
        player = GameObject.FindWithTag(Constants.PLAYER_TAG);
        camera = GameObject.FindWithTag(Constants.MAIN_CAMERA_TAG);

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
        Vector3 position = SpawnPoints.instance.CastleSpawnPoint().position;
        player.transform.position = position;
        GameObject camera = GameObject.FindWithTag(Constants.MAIN_CAMERA_TAG);
        camera.transform.position = position + player.GetComponent<CameraController>().playerCameraOffset;
        SceneManager.sceneLoaded -= LoadingMainSceneFromCastle;
    }

    void LoadingMainSceneFromBakery(Scene scene, LoadSceneMode mode)
    {
        GameObject player = GameObject.FindWithTag(Constants.PLAYER_TAG);
        player.transform.position = SpawnPoints.instance.BakerySpawnPoint().position;
        SceneManager.sceneLoaded -= LoadingMainSceneFromBakery;
    }
}