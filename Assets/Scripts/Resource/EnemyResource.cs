using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyResource : MonoBehaviour {

	public static EnemyResource instance;

	List<Enemy> enemyList = new List<Enemy>();

	Enemy newEnemy;
	// Use this for initialization
	void Awake () {

		/*
			0: General Plain
			1: General Cho
		*/
		instance = this;
		newEnemy = new Enemy ("General Plain",0,3);
		enemyList.Add (newEnemy);

		newEnemy = new Enemy ("General Cho",1,3);
		enemyList.Add (newEnemy);
	}
	
	// Update is called once per frame
	public Enemy LoadEnemy(int index) {
		return enemyList [index];
	}
}
