using UnityEngine;
using System.Collections;

public class EnemyEvent : MonoBehaviour {

	public int enemyIndex;
	public Enemy enemyMain;

	// Use this for initialization
	void Start () {
		enemyMain = EnemyResource.instance.LoadEnemy (enemyIndex);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
