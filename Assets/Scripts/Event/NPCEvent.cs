using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class NPCEvent : MonoBehaviour {

	public int npcIndex;
	public string npcName;

	public NPC npcMain;

	// Use this for initialization
	void Start () {
		npcMain = NPCResource.instance.LoadNPC (npcIndex);
		npcName = npcMain.GetName ();
	}
	
	// Update is called once per frame
	void Update () {
//		Debug.Log (npcMain.GetName ());
	}
	
}
