using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnvironmentSystem : MonoBehaviour {

	public static EnvironmentSystem instance;

	public List<Transform> environmentEasyXMovement = new List<Transform> ();
	public List<Transform> environmentEasyZMovement = new List<Transform> ();

	public List<Transform> environmentRoadPath1Movement = new List<Transform> ();
	public List<Transform> environmentRandomPathMovement = new List<Transform> ();
	public List<Transform> TrainObj;


	// Path
	int[] nearestroadPath1;
	public List<Transform> roadPath1 = new List<Transform>();

	// Railroad
	int[] railroadIndex = new int[1];
	public List<Transform> railroadPath = new List<Transform>();

	// Random Path
	int[] nearestRandom;
	public List<Transform> allTownPath = new List<Transform>();

	//values for internal use
	private Quaternion _lookRotation;
	private Vector3 _direction;

	int direction = 1;

	// Use this for initialization
	void Start () {
		instance = this;

		railroadIndex[0] = 0;
		// Create Path
		nearestroadPath1 = SearchNearestPath (environmentRoadPath1Movement,roadPath1);
		nearestRandom = SearchNearestPath (environmentRandomPathMovement, allTownPath);


		StartCoroutine (TurnAround());
	}

	void Update() {
		// For Easy Movement
		MoveForward (direction,2f);
		// Move to path
		MoveToPath (environmentRoadPath1Movement,roadPath1,nearestroadPath1,5f);
		MoveToPath (TrainObj,railroadPath, railroadIndex,1f);
		MoveToPath (environmentRandomPathMovement,allTownPath,nearestRandom,2f,true);

	}
		
	// Move Easy 
	IEnumerator TurnAround() {
		while (true) {
			yield return new WaitForSeconds(5);
			// X Movement
			foreach (Transform tCar in environmentEasyXMovement) {
				tCar.Rotate(new Vector3(0,180f,0)); 
			}
			// Z Movement
			foreach (Transform tCar in environmentEasyZMovement) {
				tCar.Rotate(new Vector3(0,180f,0)); 
			}

			direction *= -1;
		}
	}

	void MoveForward(int direct,float speed) {

		// X Movement
		foreach (Transform tCar in environmentEasyXMovement) {
			tCar.position = new Vector3 (tCar.position.x + (speed* direct) * Time.deltaTime , tCar.position.y, tCar.position.z);
		}

		// Z Movement
		foreach (Transform tCar in environmentEasyZMovement) {
			tCar.position = new Vector3 (tCar.position.x , tCar.position.y, tCar.position.z + (speed* direct) * Time.deltaTime);
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

	void MoveToPath(List<Transform> environmentPathObj, List<Transform> rPath, int[] nearestP,float speed,bool random=false) {
		
		for (int i = 0; i < environmentPathObj.Count; i++) {
			Debug.Log (environmentPathObj [i].gameObject.name);
			Vector3 targetPosition = new Vector3 (rPath [nearestP [i]].position.x, environmentPathObj [i].position.y, rPath [nearestP [i]].position.z);
			environmentPathObj [i].position = Vector3.MoveTowards (environmentPathObj [i].position, targetPosition , speed * Time.deltaTime);

			// Check end path
			if ((rPath [nearestP [i]].position.x == environmentPathObj [i].position.x) && (rPath [nearestP [i]].position.z == environmentPathObj [i].position.z)) {
				if (random) {
					nearestP [i] = (nearestP [i] + Random.Range (-1, 1)) % rPath.Count;
					if (nearestP [i] < 0)
						nearestP [i] = rPath.Count - 1;
				} else {
					nearestP[i] = (nearestP[i] + 1)% rPath.Count;
				}
				
			} 

			_direction = (targetPosition - environmentPathObj [i].position).normalized;
			_lookRotation = Quaternion.LookRotation(_direction);
			environmentPathObj [i].rotation = Quaternion.Slerp(environmentPathObj [i].rotation, _lookRotation, Time.deltaTime * 2f);
		}
	}

	// Remove from others
	public void RemoveEnvironmentControl(Transform tObject) {
		int index = environmentEasyXMovement.IndexOf(tObject);
		if (index != -1) {
			environmentEasyXMovement.RemoveAt (index);
		} else {
			index = environmentEasyZMovement.IndexOf(tObject);
			if (index != -1) {
				environmentEasyZMovement.RemoveAt (index);
			} else {
				index = environmentRandomPathMovement.IndexOf (tObject);
				if (index != -1) {
					environmentRandomPathMovement.RemoveAt (index);
				}
			}
		}
			
	}

}
