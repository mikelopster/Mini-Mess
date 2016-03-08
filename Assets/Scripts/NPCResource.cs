using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class NPCResource : MonoBehaviour {

	public static NPCResource instance;

	public List<NPC> npcData = new List<NPC>();

	// Use this for initialization
	void Awake () {

		instance = this;

		NPC newNPC = new NPC ("Earth");
		string talkD = "Hello, My name is Earth!";
		Dictionary<int, Dictionary<int,string>> newTalk = new Dictionary<int, Dictionary<int,string>> ();
		newTalk [0] = new Dictionary<int, string> ();
		newTalk[0][2] = "Talk back to Pay!";
		newNPC.SetTalk (talkD, newTalk);
		npcData.Add (newNPC);

		newNPC = new NPC ("Pay");
		talkD = "What do you want to eat?";
		newTalk = new Dictionary<int, Dictionary<int,string>> ();
		newTalk [0] = new Dictionary<int, string> ();
		newTalk[0][0] = "Do you want to get Quest ?";
		newTalk[0][1] = "Let's talk to Earth Major!";
		newTalk[0][3] = "End Quest";
		newNPC.SetTalk (talkD, newTalk);
		npcData.Add (newNPC);
//
	}

	public NPC LoadNPC(int index) {
		return npcData [index];
	}
	

}
