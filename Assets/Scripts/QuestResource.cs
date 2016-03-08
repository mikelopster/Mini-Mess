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

	List<int> npcs = new List<int>();
	float bounty;
	float gold;
	Quest newQuest;

	// Use this for initialization
	void Awake () {

		instance = this;

		// Create Resource
		newQuest = new Quest(50f,2000f,new List<int>(new int[]{1,0,1}));
		questData.Add (newQuest);
		questStatus.Add (0);
		questNPC.Add (1);

		newQuest = new Quest(100f,200f,new List<int>(new int[]{0,1}));
		questData.Add (newQuest);
		questStatus.Add (-1);
		questNPC.Add (0);

	}

	void Update() {
		Debug.Log ("Quest 0: " + questStatus[0] + "," + questNPC[0]);
	}

	public int[] GetQuestIndex(int npcIndex) {

		int index = questNPC.IndexOf (npcIndex);

		Debug.Log ("Search: " + index);

		if (index == -1)
			return new int[]{ index, index };
		else {
			int questStatusNumber = questStatus [index];
			Debug.Log ("Status: " + questStatusNumber);
			return new int[]{ index, questStatusNumber};
		}
	}

	public void SetQuestStatus(int qIndex, int qStatus, int qNPC) {
		questStatus [qIndex] = qStatus;
		questNPC [qIndex] = qNPC;
	}

	public Quest LoadQuest(int qIndex) {
		return questData [qIndex];
	}

}
