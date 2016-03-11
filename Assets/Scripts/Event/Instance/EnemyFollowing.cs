using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyFollowing : MonoBehaviour {

	public List<Transform> enemyFollowing;
	public static EnemyFollowing instance;
	public Transform Mike;
	// Use this for initialization

	//values for internal use
	private Quaternion _lookRotation;
	private Vector3 _direction;


	void Awake () {
		instance = this;
	}

	void Start() {
		Mike = GameObject.FindWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		
		MoveToPlayer ();
	}

	public void AddFollowing(Transform tEnemy) {
		enemyFollowing.Add (tEnemy);
	}

	public void RemoveFollowing(Transform tEnemy) {
		int index = enemyFollowing.IndexOf(tEnemy);
		Debug.Log ("Remove: " + index);
		if (index != -1) {
			enemyFollowing.RemoveAt (index);
		}
	}

	public void MoveToPlayer() {
		for (int i = 0; i < enemyFollowing.Count; i++) {

			
			Vector3 targetPosition = new Vector3 (Mike.position.x, enemyFollowing[i].position.y, Mike.position.z);
			enemyFollowing[i].position = Vector3.MoveTowards (enemyFollowing[i].position, targetPosition , 0.5f * Time.deltaTime);

			_direction = (targetPosition - enemyFollowing[i].position).normalized;
			_lookRotation = Quaternion.LookRotation(_direction);
			enemyFollowing[i].rotation = Quaternion.Slerp(enemyFollowing [i].rotation, _lookRotation, Time.deltaTime * 2f);
		}
	}
}


