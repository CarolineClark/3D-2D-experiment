using UnityEngine;
using  UnityEngine.SceneManagement;

public class BackToTownTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		SceneLoaderManager.instance.LoadMainSceneAtCastle();
	}
}
