using UnityEngine;
using UnityEngine.UI;

using System.Collections;

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
				// Ask Quest
				UIController.instance.TalkUIQuest(npcMain.GetName (), npcMain.GetTalkQuest(questPoint[0],questPoint[1]));
				Debug.Log(currentNPC.GetName() + ": " + currentNPC.GetTalkQuest(questPoint[0],questPoint[1]));
			}else {
				UIController.instance.TalkUIDefault (npcMain.GetName (), currentNPC.GetTalkQuest(questPoint[0],questPoint[1]));
			}
			
		}
	}


	public void AcceptQuest() {
		// Accept
		questPoint[1]++;
		UIController.instance.ResetUI ();
		UIController.instance.TalkUIDefault (currentNPC.GetName (), currentNPC.GetTalkQuest (questPoint[0],questPoint[1]));

		// Update NPC
		Quest currentQuest = QuestResource.instance.LoadQuest(questPoint[0]);
		int nextNPC = currentQuest.GetNPCInQuest (questPoint [1]);
		Debug.Log ("Next: " + questPoint [0] + "," + ++questPoint [1] + "," + nextNPC);

		// Update Status
		QuestResource.instance.SetQuestStatus(questPoint[0],questPoint[1],nextNPC);

	}


}
