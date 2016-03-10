using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class Quest {

	// Talk with person 
	List<int> npcIndexList = new List<int>();
	List<int> unlockQuestList = new List<int>();
	List<string> npcTypeList = new List<string> ();

	// Get Item

	// Kill People
		
	// Reward
	// Bounty, Gold, Weapon
	float bounty;
	float gold;

	public Quest(float bounty, float gold, List<int> npcList, List<int> unlockQ, List<string> npcTList) {
		this.bounty = bounty;
		this.gold = gold;
		this.npcIndexList = npcList;
		this.unlockQuestList = unlockQ;
		this.npcTypeList = npcTList;
	}

	public int GetNPCInQuest(int qIndex) {
		if (qIndex >= npcIndexList.Count) {
			return 999;
		} else {
			return npcIndexList [qIndex];
		}
	}

	public string GetCurrentQuestType(int qIndex) {
		if (qIndex >= npcTypeList.Count) {
			// Default is talk
			return "Talk";
		} else {
			return npcTypeList [qIndex];
		}
	}

	public List<int> GetUnlockList() {
		return unlockQuestList;
	}

	public float GetGold() {
		return this.gold;
	}


}
