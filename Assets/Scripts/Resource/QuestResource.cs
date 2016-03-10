using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class QuestResource : MonoBehaviour {

	public static QuestResource instance;
	// List Quest Data
	public List<Quest> questData = new List<Quest>();
	// List Quest Status
	public List<int> questStatus = new List<int>();
	public List<int> questNPC = new List<int> ();
	public List<string> questType = new List<string>();

	float bounty;
	float gold;
	Quest newQuest;
	string qStatus;

	// Use this for initialization
	void Awake () {

		instance = this;

		// NPC
		/*
		 0: Kitti
		 1: Earth
		 2: Ben
		 3: Pay
		 */
	

		newQuest = new Quest (50f, 2000f, new List<int> (new int[]{1,0,0,0}), new List<int> (new int[]{1}), new List<string> (new string[]{"Talk","Talk","Enemy","Talk"}));
		questData.Add (newQuest);
		questStatus.Add (0);
		questNPC.Add (1);
		questType.Add ("Talk");

		newQuest = new Quest (50f, 2000f, new List<int> (new int[]{3,2,1,1}), new List<int> (), new List<string> (new string[]{"Talk","Talk","Enemy","Talk"}));
		questData.Add (newQuest);
		questStatus.Add (0);
		questNPC.Add (3);
		questType.Add ("Talk");


	}

	void Update() {
		qStatus = "";
		qStatus += "Quest 0: Order=" + questStatus [0] + ",NPC=" + questNPC [0] + ",Type=" + questType [0] + "\n";
		qStatus += "Quest 1: Order=" + questStatus [1] + ",NPC=" + questNPC [1] + ",Type=" + questType [1];
		Debug.Log (qStatus);
	}

	public int[] GetQuestIndex(int npcIndex) {

		int index = -1;

		// Search 
		for (int i = 0; i < questNPC.Count; i++) {
			if(questNPC[i] == npcIndex) {
				// Found NPC
				if (questStatus [i] != -1) {
					// Next NPC
					index = i;
					break;
				}
			}
		}
			

		// int index = .IndexOf (npcIndex);


		Debug.Log ("Search: " + index);

		if (index == -1)
			return new int[]{ index, index };
		else {
			int questStatusNumber = questStatus [index];
			Debug.Log ("Status: " + questStatusNumber);
			return new int[]{ index, questStatusNumber};
		}
	}

	public void SetQuestStatus(int qIndex, int qStatus, string qType ,int qNPC) {
		questStatus [qIndex] = qStatus;
		questNPC [qIndex] = qNPC;
		questType [qIndex] = qType;
	}

	public void UnlockQuest(int qIndex) {
		questStatus [qIndex] = 0;
	}

	public Quest LoadQuest(int qIndex) {
		return questData [qIndex];
	}

}
