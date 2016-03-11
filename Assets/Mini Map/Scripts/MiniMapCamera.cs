using UnityEngine;
using System.Collections;

public class MiniMapCamera : MonoBehaviour {

	public Transform head;
	public Transform body;

	Vector3 position;
	Vector3 rotation;

	void Awake () {
		position = new Vector3 ();
		rotation = new Vector3 ();
	}

	void Update () {
		position.Set (head.position.x, transform.position.y, head.position.z);
		rotation.Set (transform.eulerAngles.x, body.eulerAngles.y, transform.eulerAngles.z);

		transform.position = position;
		transform.eulerAngles = rotation;
	}
}
