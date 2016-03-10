using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyStar : MonoBehaviour {

	public static EnemyStar instance;

	public Transform Mike;
	public List<GameObject> Army1 = new List<GameObject> ();
	public List<Transform> BattleArmy = new List<Transform> ();
	bool[] ArmyTroop = new bool[5];

	//values for internal use
	private Quaternion _lookRotation;
	private Vector3 _direction;

	void Awake() {
		instance = this;
	}

	void Start() {
		Mike = GameObject.FindWithTag("Player").transform;
		ClearArmy ();
	}
	// Update is called once per frame
	void Update () {
		ArmyMovement (BattleArmy);
	}

	public void SpawnArmy(int star) {
		Debug.Log("Spawn Star: " + star);
	
		if (star == 1 && !ArmyTroop [star]) {
			ArmyTroop [star] = true;
			// Spawn Army1
			Debug.Log("Spawn Army");

			for (int i = 0; i < Army1.Count; i++) {
				GameObject newArmy = (GameObject)Instantiate (Army1 [i], new Vector3 (Mike.position.x + 5f, Mike.position.y, Mike.position.z + 5f),Mike.rotation);
				BattleArmy.Add (newArmy.transform);
			}
		}
	}

	public void ClearArmy() {
		for (int i = 0; i < ArmyTroop.Length; i++) {
			ArmyTroop [i] = false;
		}
	}

 	void ArmyMovement(List<Transform> tArmy) {
		for (int i = 0; i < tArmy.Count; i++) {

			Vector3 targetPosition = new Vector3 (Mike.position.x, Mike.position.y, Mike.position.z);
			tArmy[i].position = Vector3.MoveTowards (tArmy[i].position, targetPosition , 2f * Time.deltaTime);

			_direction = (targetPosition - tArmy[i].position).normalized;
			_lookRotation = Quaternion.LookRotation(_direction);
			tArmy[i].rotation = Quaternion.Slerp(tArmy [i].rotation, _lookRotation, Time.deltaTime * 2f);
		}
	}

}
