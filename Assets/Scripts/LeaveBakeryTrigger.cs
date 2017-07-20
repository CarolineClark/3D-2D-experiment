using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.SceneManagement;

public class LeaveBakeryTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		SceneManager.LoadScene(Constants.MAIN_SCENE);
	}
}