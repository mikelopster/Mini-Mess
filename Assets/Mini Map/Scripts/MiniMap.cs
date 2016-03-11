using UnityEngine;
using System.Collections;

public class MiniMap : MonoBehaviour {

	public GameObject map;

	void Start () {
		map.SetActive (false);
	}

	void Update () {
		if (Input.GetButtonDown ("Mini Map")) {
			bool active = map.activeInHierarchy;
			map.SetActive (!active);
		}
	}
}
