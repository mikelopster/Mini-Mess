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
		string talkD = "Hello. I'm major. I want to start a revolution!!!";
		Dictionary<int, Dictionary<int,string>> newTalk = new Dictionary<int, Dictionary<int,string>> ();
		newTalk [0] = new Dictionary<int, string> ();
		newTalk[0][2] = "Kill General Claim, he is walking in vice city!";
		newTalk[0][4] = "OK. You are a relovutioner.";
		newNPC.SetTalk (talkD, newTalk);
		npcData.Add (newNPC);

		newNPC = new NPC ("Earth");
		talkD = "Hello Mike, want to join with me ?";
		newTalk = new Dictionary<int, Dictionary<int,string>> ();
		newTalk [0] = new Dictionary<int, string> ();
		newTalk[0][0] = "Do you want to revolution?";
		newTalk[0][1] = "Let's talk to Kitti, he is on a concert.";
		newNPC.SetTalk (talkD, newTalk);
		npcData.Add (newNPC);

		newNPC = new NPC ("Ben");
		talkD = "Want to handjob ?";
		newTalk = new Dictionary<int, Dictionary<int,string>> ();
		newTalk [1] = new Dictionary<int, string> ();
		newTalk[1][2] = "Battle!";
		newTalk[1][4] = "Great, you are strong!";
		newNPC.SetTalk (talkD, newTalk);
		npcData.Add (newNPC);

		newNPC = new NPC ("Pay");
		talkD = "You can get a ticker in the next version.";
		newTalk = new Dictionary<int, Dictionary<int,string>> ();
		newTalk [1] = new Dictionary<int, string> ();
		newTalk[1][0] = "Do you want to test your power?";
		newTalk[1][1] = "OK! Let's go to Ben.";
		newNPC.SetTalk (talkD, newTalk);
		npcData.Add (newNPC);

	
//
		Debug.Log("LoadNPC");
	}

	public NPC LoadNPC(int index) {
		return npcData [index];
	}
	

}
