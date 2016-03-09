using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCGenerator : MonoBehaviour {

	public List<GameObject> NPCObject = new List<GameObject>();
	GameObject newNPC;

	// Use this for initialization
	void Start () {
		foreach (GameObject npc in NPCObject) {
			newNPC = (GameObject)Instantiate (npc,npc.transform.position,npc.transform.rotation);
			newNPC.transform.SetParent (transform);
			newNPC.transform.localPosition =  npc.transform.position;

			Debug.Log ("NPC: " + newNPC.transform.position);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
