using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.SceneManagement;

public class EnterBakeryTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		SceneManager.LoadScene(Constants.BAKERY_SCENE);
	}
}
