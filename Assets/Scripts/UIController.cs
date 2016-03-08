using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class UIController : MonoBehaviour {

	public static UIController instance;
	public Text UIName,UIText;
	public GameObject AcceptBtn,CancelBtn,OKBtn;

	// Use this for initialization

	void Awake() {
		instance = this;
	}

	void Start () {
		ResetUI ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Z)) {
			ResetUI ();
		}
	}

	// Talk Default
	public void TalkUIDefault(string name,string dialog) {
		instance.gameObject.SetActive (true);
		OKBtn.SetActive (true);
		UIName.text = name;
		UIText.text = dialog;
	}

	// Talk for Accept or Cancel
	public void TalkUIQuest(string name,string dialog) {
		instance.gameObject.SetActive (true);
		AcceptBtn.SetActive (true);
		CancelBtn.SetActive (true);
		UIName.text = name;
		UIText.text = dialog;
	}


	public void ResetUI() {
		instance.gameObject.SetActive (false);
		AcceptBtn.SetActive (false);
		CancelBtn.SetActive (false);
		OKBtn.SetActive (false);
	}


}
