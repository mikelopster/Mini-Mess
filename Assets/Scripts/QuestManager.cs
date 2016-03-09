using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour {
	public static QuestManager instance;

	NPC currentNPC;
	int[] questPoint;


	void Start() {
		instance = this;
	}

	public void CheckQuest(int npcIndex,NPC npcMain) {
		// Check Status
		questPoint = QuestResource.instance.GetQuestIndex(npcIndex);
		currentNPC = npcMain;

		// QuestIndex, QuestStatus
		Debug.Log(questPoint[0] + ", " + questPoint[1]);

		if (questPoint[1] == -1) {
			// cannot get quest
			UIController.instance.TalkUIDefault (npcMain.GetName (), npcMain.GetTalkDefault ());
			Debug.Log (currentNPC.GetName () + ": " + currentNPC.GetTalkDefault ());
		} 
		else {
			if(questPoint[1] == 0) {
				// Ask Quest for start
				UIController.instance.TalkUIQuest(npcMain.GetName (), npcMain.GetTalkQuest(questPoint[0],questPoint[1]));
				Debug.Log(currentNPC.GetName() + ": " + currentNPC.GetTalkQuest(questPoint[0],questPoint[1]));
			}else {
				// Get to next npc
				UIController.instance.TalkUIDefault (npcMain.GetName (), currentNPC.GetTalkQuest(questPoint[0],questPoint[1]));
				UpdateQuest ();
			}
			
		}
	}


	public void AcceptQuest() {
		// Accept
		questPoint[1]++;
		UIController.instance.ResetUI ();
		UIController.instance.TalkUIDefault (currentNPC.GetName (), currentNPC.GetTalkQuest (questPoint[0],questPoint[1]));
		UpdateQuest ();
	}

	public void UpdateQuest() {
		// Update NPC
		Quest currentQuest = QuestResource.instance.LoadQuest(questPoint[0]);
		int nextNPC = currentQuest.GetNPCInQuest (questPoint [1]);
		if (nextNPC != 999) // Check last quest
			questPoint [1]++; // Update to next
		else {
			// End Quest
			questPoint [1] = 999;

			// Unlock Next Quest
			List<int> unlockL = currentQuest.GetUnlockList();
			UpdateQuestList (unlockL);
		}
		
	
		Debug.Log ("Next: " + questPoint [0] + "," + questPoint [1] + "," + nextNPC);

		// Update Status
		QuestResource.instance.SetQuestStatus(questPoint[0],questPoint[1],nextNPC);

	}


	public void UpdateQuestList(List<int> unlockL) {
		int unlockLength = unlockL.Count;

		for (int i = 0; i < unlockLength; i++) {
			QuestResource.instance.UnlockQuest (unlockL [i]);
		}
	}


}
