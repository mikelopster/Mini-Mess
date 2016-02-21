using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class Quest {

	// Condition
	public int index;
	public int process = 0;

	// Talk with person 
	List<NPC> npcList = new List<NPC>();
	NPC startNPC;
	NPC destinationNPC;

	// Get Item

	// Kill People
		
	// Reward
	// Bounty, Gold, Weapon
	float bounty;
	float gold;

	public Quest(int index,float bounty, float gold, List<NPC> npcList) {
		this.index = index;
		this.bounty = bounty;
		this.gold = gold;
		this.npcList = npcList;
		this.process = 1;
	}

	public void SetMainNPC(NPC start, NPC end) {
		this.startNPC = start;
		this.destinationNPC = end;
	}

	public int[] TalkNPC(NPC npc) {
		int quest_index = npcList.IndexOf (npc);

		if (quest_index == process - 1) {
			// Right NPC
			int[] return_data = { index,process++ } ;
			return return_data;
		} 
		else {
			// NPC Undefined
			if(process > npcList.Count) {
				// Check Destination NPC
				bool finish = FinishNPC(npc);
				if(finish) {
					// Finish Quest
					npc.FinishQuest();
					int[] return_data =  { index,999 };
					return return_data;
				}
			}

			int[] fail_data = {-1};
			return fail_data;
		}
	}

	bool FinishNPC(NPC npc) {
		return npc == this.destinationNPC;
	}
		


}
