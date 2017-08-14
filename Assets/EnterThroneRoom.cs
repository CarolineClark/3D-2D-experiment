using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterThroneRoom : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		SceneManager.LoadScene(Constants.THONE_ROOM_SCENE);
	}
}
