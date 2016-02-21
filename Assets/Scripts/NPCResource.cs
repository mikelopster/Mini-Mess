using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class NPCResource : MonoBehaviour {
	public static List<NPC> npcData = new List<NPC>();

	// Use this for initialization
	void Awake () {
	
		NPC newNPC = new NPC ("Earth");
		string talkD = "Hello, My name is Earth!";
		Dictionary<int, Dictionary<int,string>> newTalk = new Dictionary<int, Dictionary<int,string>> ();
		newTalk [0] = new Dictionary<int, string> ();
		newTalk[0][0] = "Start Quest";
		newTalk[0][999] = "End Quest";
		newNPC.SetTalk (talkD, newTalk);
		npcData.Add (newNPC);

		newNPC = new NPC ("Pay");
		talkD = "What do you want to eat?";
		newTalk = new Dictionary<int, Dictionary<int,string>> ();
		newTalk [0] = new Dictionary<int, string> ();
		newTalk[0][1] = "Talk back to Earth!";
		newNPC.SetTalk (talkD, newTalk);
		npcData.Add (newNPC);

	}

	public static NPC LoadNPC(int index) {
		return npcData [index];
	}
	

}
