using UnityEngine;
using System.Collections;

public class MovementSync : MonoBehaviour {


	public Transform endMarker;
	public float speed = 1.0F;
	private float startTime;
	private float journeyLength;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
		journeyLength = Vector3.Distance(transform.position, endMarker.position);
	}
	
	// Update is called once per frame
	void Update () {
		float distCovered = (Time.time - startTime) * speed;
		float fracJourney = distCovered / journeyLength;
		transform.position = Vector3.Lerp(transform.position, endMarker.position, fracJourney);
	}

	void MoveSync() {

	}
}
