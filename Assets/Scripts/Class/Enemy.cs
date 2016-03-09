using UnityEngine;
using System.Collections;

public class Enemy {

	// Property
	string name;
	int targetQuest;
	int targetIndexOrder;

	public Enemy(string n,int tQuest,int tOrder) {
		this.name = n;
		targetQuest = tQuest;
		targetIndexOrder = tOrder;
	}


	public bool CheckQuest(int tQuest, int tOrder) {
		if (targetQuest == tQuest && targetIndexOrder == tOrder) {
			return true;
		} else {
			return false;
		}
	}


}
