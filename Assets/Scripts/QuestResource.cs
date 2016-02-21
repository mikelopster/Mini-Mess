using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class QuestResource : MonoBehaviour {

	public static List<Quest> questData = new List<Quest>();

	// Use this for initialization
	void Start () {
		// Set Quest Data
		List<NPC> npcs = new List<NPC> ();
		npcs.Add (NPCResource.LoadNPC (1));
		Quest newQuest = new Quest (0,10f,10f,npcs);
		newQuest.SetMainNPC (NPCResource.LoadNPC (0), NPCResource.LoadNPC (0));

		// Add Quest to NPC, questData
		NPC currentNPC = NPCResource.LoadNPC (0);
		currentNPC.AddQuest (newQuest);
		questData.Add (newQuest);

	}


}
