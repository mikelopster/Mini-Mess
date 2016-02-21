using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class NPC {

	// Talk List
	Dictionary<int, Dictionary<int,string>> talkQuests = new Dictionary<int, Dictionary<int,string>>();
	string talkDefault;

	// Quest List
	Queue<Quest> questQueue = new Queue<Quest>();
	bool runningQuest;

	// Attr
	string name;

	public NPC(string name) {
		this.name = name;
		this.runningQuest = false;
	}

	public string GetName() {
		return this.name;
	}
	

	// Talk Method
	public void SetTalk(string talkD,Dictionary<int, Dictionary<int,string>> talkQs) {
		this.talkDefault = talkD;
		this.talkQuests = talkQs;
	}

	public string GetTalkDefault() {
		return talkDefault;
	}

	public string GetTalkQuest(int quest_index,int talk_index) {
		return talkQuests [quest_index][talk_index];
	}


	// Quest Method
	public Quest GetQuest() {
		if (questQueue.Count > 0) {
			this.runningQuest = true;
			return questQueue.Dequeue();
		}
		else
			return null;
	}

	public bool RunningQuest() {
		return this.runningQuest;
	}

	public void AddQuest(Quest quest) {
		questQueue.Enqueue (quest);
	}

	public void FinishQuest() {
		this.runningQuest = false;
	}

}
