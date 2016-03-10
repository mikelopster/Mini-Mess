using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class NPCResource : MonoBehaviour {

	public static NPCResource instance;

	public List<NPC> npcData = new List<NPC>();

	// Use this for initialization
	void Awake () {

		instance = this;

		/*
		 0: Kitti
		 1: Earth
		 2: Ben
		 3: Pay
		 */


		NPC newNPC = new NPC ("Kitti");
		string talkD = "Hello Mike, want to join with me ?";
		Dictionary<int, Dictionary<int,string>> newTalk = new Dictionary<int, Dictionary<int,string>> ();
//		newTalk [0] = new Dictionary<int, string> ();
//		newTalk[0][2] = "Talk back to Pay!";
//		newTalk [1] = new Dictionary<int, string> ();
//		newTalk[1][0] = "Clear Quest 0 already ? ok next to talk with pay again ?";
//		newTalk[1][1] = "OK! Let's GO!";
		newNPC.SetTalk (talkD, newTalk);
		npcData.Add (newNPC);

		newNPC = new NPC ("Earth");
		talkD = "What do you want to eat?";
		newTalk = new Dictionary<int, Dictionary<int,string>> ();
//		newTalk [0] = new Dictionary<int, string> ();
//		newTalk[0][0] = "Do you want to get Quest ?";
//		newTalk[0][1] = "Let's talk to Earth Major!";
//		newTalk[0][3] = "End Quest";
//		newTalk [1] = new Dictionary<int,string> ();
//		newTalk[1][3] = "Thank you mike! (End Quest)";
		newNPC.SetTalk (talkD, newTalk);
		npcData.Add (newNPC);
	
//
	}

	public NPC LoadNPC(int index) {
		return npcData [index];
	}
	

}
