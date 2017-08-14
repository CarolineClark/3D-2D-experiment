using UnityEngine;
using  UnityEngine.SceneManagement;

public class BackToTownTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		SceneManager.LoadScene(Constants.MAIN_SCENE);
	}
}
