using UnityEngine;
using System.Collections;

public class CursonController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void CloseMouseTurn() {
		Cursor.lockState = CursorLockMode.Locked;
	}
}
