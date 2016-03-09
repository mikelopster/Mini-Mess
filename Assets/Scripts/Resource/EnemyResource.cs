using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyResource : MonoBehaviour {

	public static EnemyResource instance;

	List<Enemy> enemyList = new List<Enemy>();

	Enemy newEnemy;
	// Use this for initialization
	void Awake () {
		instance = this;
		newEnemy = new Enemy ("Enemy A",1,2);
		enemyList.Add (newEnemy);
	}
	
	// Update is called once per frame
	public Enemy LoadEnemy(int index) {
		return enemyList [index];
	}
}
