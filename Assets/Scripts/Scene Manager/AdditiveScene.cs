using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AdditiveScene : MonoBehaviour {

	public string name;
	public string tag;

	void OnTriggerEnter (Collider other) {
		if (other.CompareTag (tag)) {
			if (!SceneManager.GetSceneByName (name).isLoaded)
				StartCoroutine (LoadAdditiveScene ());
		}
	}

	void OnTriggerExit (Collider other) {
		if (other.CompareTag (tag)) {
			if (SceneManager.GetSceneByName (name).isLoaded)
				StartCoroutine (UnLoadAdditiveScene ());
		}
	}

	IEnumerator LoadAdditiveScene () {
		yield return new WaitForSeconds (0.1f);
		SceneManager.LoadSceneAsync (name, LoadSceneMode.Additive);
	}

	IEnumerator UnLoadAdditiveScene () {
		yield return new WaitForSeconds (0.1f);
		SceneManager.UnloadScene (name);
	}
}
