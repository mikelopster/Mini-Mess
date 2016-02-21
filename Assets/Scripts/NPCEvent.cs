using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCEvent : MonoBehaviour {

	public int npcIndex;
	NPC npcMain;
	Quest currentQuest;

	// Use this for initialization
	void Start () {
		npcMain = NPCResource.LoadNPC (npcIndex);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnMouseDown() {

		bool checkQ = CheckQuest ();

		if (!checkQ) {
			currentQuest = LoadQuest ();


			if (currentQuest != null) {
				CharacterQuestEvent charQ = GameObject.Find ("Character").GetComponent<CharacterQuestEvent> ();
				charQ.AddQuest (currentQuest);
				Debug.Log (npcMain.GetTalkQuest (currentQuest.index, 0));
			} else {
				Debug.Log (npcMain.GetTalkDefault ());
			}
		}

	}

	Quest LoadQuest() {
		if (npcMain.RunningQuest () == false)
			return npcMain.GetQuest ();
		else 
			return null;
	}

	bool CheckQuest() {
		CharacterQuestEvent charQ = GameObject.Find ("Character").GetComponent<CharacterQuestEvent>();
		List<Quest> qList = charQ.questList;
		for (int i = 0; i < qList.Count; i++) {
			int[] result = qList[i].TalkNPC(npcMain);
			if(result.Length == 2) {
				Debug.Log ("R: " + result[0].ToString() + "," +result[1].ToString());
				Debug.Log (npcMain.GetTalkQuest(result[0],result[1]));
				if(result[1] == 999) {
					qList.RemoveAt(i);
				}
				return true;
			}
		}

		return false;
	}
	
}
