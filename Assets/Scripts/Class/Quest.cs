using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class Quest {

	// Talk with person 
	List<int> npcIndexList = new List<int>();

	// Get Item

	// Kill People
		
	// Reward
	// Bounty, Gold, Weapon
	float bounty;
	float gold;

	public Quest(float bounty, float gold, List<int> npcList) {
		this.bounty = bounty;
		this.gold = gold;
		this.npcIndexList = npcList;
	}

	public int GetNPCInQuest(int qIndex) {
		return npcIndexList [qIndex];
	}


}
