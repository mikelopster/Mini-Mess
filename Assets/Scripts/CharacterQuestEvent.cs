using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CharacterQuestEvent : MonoBehaviour {

	public List<Quest> questList = new List<Quest>();
		
	// Use this for initialization
	void Start () {
		// Update is called once per frame
	}
	
		
	void Update () {
		//	Debug.Log (questList.Count);
	}

	public void AddQuest(Quest quest) {
		questList.Add (quest);
	}
	
}
