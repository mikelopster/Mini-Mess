using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAttacking : MonoBehaviour {

	public List<Transform> enemyList = new List<Transform>();
	public Transform Mike;
	bool onEnabled = false;

	//values for internal use
	private Quaternion _lookRotation;
	private Vector3 _direction;

	// Use this for initialization
	void Start () {
		onEnabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (onEnabled) {
			EnemyATK ();
		}
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Main Character") {
			Debug.Log(col.gameObject.tag);
			onEnabled = true;
//			Debug.Log ("Trigger!");
		}
	}

	void OnTriggerExit(Collider col) {
		if (col.gameObject.tag == "Main Character") {
			onEnabled = false;
			Debug.Log ("Trigger Exit");
		}
	}

	void EnemyATK() {
		for (int i = 0; i < enemyList.Count; i++) {

			Vector3 targetPosition = new Vector3 (Mike.position.x, enemyList[i].position.y, Mike.position.z);
			enemyList[i].position = Vector3.MoveTowards (enemyList[i].position, targetPosition , 0.5f * Time.deltaTime);

			_direction = (targetPosition - enemyList[i].position).normalized;
			_lookRotation = Quaternion.LookRotation(_direction);
			enemyList[i].rotation = Quaternion.Slerp(enemyList [i].rotation, _lookRotation, Time.deltaTime * 2f);

		}
	}
}
