using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnvironmentSystem : MonoBehaviour {

	public List<Transform> environmentEasyMovement = new List<Transform> ();
	public List<Transform> environmentPathMovement = new List<Transform> ();

	// Path
	public int[] nearestPath;
	public List<Transform> roadPath1 = new List<Transform>();

	//values for internal use
	private Quaternion _lookRotation;
	private Vector3 _direction;

	int direction = 1;

	// Use this for initialization
	void Start () {
		nearestPath = new int[environmentPathMovement.Count];
		SearchNearestPath ();

		StartCoroutine (TurnAround());
	}

	void Update() {
		// For Easy Movement
		MoveForward (direction);
		MoveToPath ();
	}



	// Move Easy 
	IEnumerator TurnAround() {
		while (true) {
			yield return new WaitForSeconds(5);

			foreach (Transform tCar in environmentEasyMovement) {
				tCar.Rotate(new Vector3(0,180f,0)); 
			}

			direction *= -1;
		}
	}

	void MoveForward(int direct) {
		foreach (Transform tCar in environmentEasyMovement) {
			tCar.position = new Vector3 (tCar.position.x + (2f* direct) * Time.deltaTime , tCar.position.y, tCar.position.z);
		}
	}

	// Move Path
	void SearchNearestPath() {
		float nearP = 1000f;
		int index = -1;


		for (int i = 0; i < environmentPathMovement.Count; i++) {
			// Loop All Object
			nearP = 1000f;
			index = -1;
			for (int j = 0; j < roadPath1.Count; j++) {
				// Loop All Path
				float dist = Vector3.Distance(environmentPathMovement[i].position,roadPath1[j].position);

				if (dist < nearP) {
					nearP = dist;
					index = j;
				}
			}
			nearestPath [i] = index;
		}
	}

	void MoveToPath() {
		for (int i = 0; i < environmentPathMovement.Count; i++) {
			

				
			Vector3 targetPosition = new Vector3 (roadPath1 [nearestPath [i]].position.x, environmentPathMovement [i].position.y, roadPath1 [nearestPath [i]].position.z);
			environmentPathMovement [i].position = Vector3.MoveTowards (environmentPathMovement [i].position, targetPosition , 2f * Time.deltaTime);

			if ((roadPath1 [nearestPath [i]].position.x == environmentPathMovement [i].position.x) && (roadPath1 [nearestPath [i]].position.z == environmentPathMovement [i].position.z)) {
				nearestPath [i] = (nearestPath [i] + 1) % (roadPath1.Count);
			} 

			_direction = (targetPosition - environmentPathMovement [i].position).normalized;
			_lookRotation = Quaternion.LookRotation(_direction);
			environmentPathMovement [i].rotation = Quaternion.Slerp(environmentPathMovement [i].rotation, _lookRotation, Time.deltaTime * 2f);
		}
	}

}
