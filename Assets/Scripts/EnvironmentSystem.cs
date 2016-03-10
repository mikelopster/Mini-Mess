using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnvironmentSystem : MonoBehaviour {

	public static EnvironmentSystem instance;

	public List<Transform> environmentEasyMovement = new List<Transform> ();
	public List<Transform> environmentroadPath1Movement = new List<Transform> ();
	public List<Transform> TrainObj;

	// Path
	int[] nearestroadPath1;
	public List<Transform> roadPath1 = new List<Transform>();

	// Railroad
	int[] railroadIndex = new int[1];
	public List<Transform> railroadPath = new List<Transform>();

	//values for internal use
	private Quaternion _lookRotation;
	private Vector3 _direction;

	int direction = 1;

	// Use this for initialization
	void Start () {
		instance = this;

		railroadIndex[0] = 0;
		// Create Path
		nearestroadPath1 = SearchNearestPath (environmentroadPath1Movement,roadPath1);


		StartCoroutine (TurnAround());
	}

	void Update() {
		// For Easy Movement
		MoveForward (direction);
		MoveToPath (environmentroadPath1Movement,roadPath1,nearestroadPath1);
		MoveToPath (TrainObj,railroadPath, railroadIndex);
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
	int[] SearchNearestPath(List<Transform> environmentPathObj, List<Transform> rPath) {
		float nearP = 1000f;
		int index = -1;
		int[] nearestPath = new int[environmentPathObj.Count];

		for (int i = 0; i < environmentPathObj.Count; i++) {
			// Loop All Object
			nearP = 1000f;
			index = -1;
			for (int j = 0; j < rPath.Count; j++) {
				// Loop All Path
				float dist = Vector3.Distance(environmentPathObj[i].position,rPath[j].position);

				if (dist < nearP) {
					nearP = dist;
					index = j;
				}
			}
			nearestPath [i] = index;
		}

		return nearestPath;
	}

	void MoveToPath(List<Transform> environmentPathObj, List<Transform> rPath, int[] nearestP) {
		
		for (int i = 0; i < environmentPathObj.Count; i++) {
			Debug.Log (environmentPathObj [i].gameObject.name);
			Vector3 targetPosition = new Vector3 (rPath [nearestP [i]].position.x, environmentPathObj [i].position.y, rPath [nearestP [i]].position.z);
			environmentPathObj [i].position = Vector3.MoveTowards (environmentPathObj [i].position, targetPosition , 2f * Time.deltaTime);

			// Check end path
			if ((rPath [nearestP [i]].position.x == environmentPathObj [i].position.x) && (rPath [nearestP [i]].position.z == environmentPathObj [i].position.z)) {
				nearestP [i] = (nearestP [i] + 1) % (rPath.Count);
			} 

			_direction = (targetPosition - environmentPathObj [i].position).normalized;
			_lookRotation = Quaternion.LookRotation(_direction);
			environmentPathObj [i].rotation = Quaternion.Slerp(environmentPathObj [i].rotation, _lookRotation, Time.deltaTime * 2f);
		}
	}

	public void RemoveEnvironmentControl(Transform tObject) {
		int index = environmentEasyMovement.IndexOf(tObject);
		if (index != -1) {
			environmentEasyMovement.RemoveAt (index);
		}
	}

}
