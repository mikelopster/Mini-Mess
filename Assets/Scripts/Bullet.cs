using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public float speed = 10;
	private float startTime;
	public int damage = 10;

	void Start() {
		startTime = Time.time;
	}

	void FixedUpdate() {
		transform.Translate(Vector3.forward * speed * Time.deltaTime);
		if (Time.time - startTime > 5)
			Destroy (gameObject);
	}

	void OnCollisionEnter (Collision col) {
		if (col.transform.tag == "Enemy") {
			Debug.Log ("FFF");
			Life l = col.gameObject.GetComponent<Life>();

			if(l != null)
				l.TakeDamage (damage);
		}
		else if (col.transform.tag == "Player") 
		{
			col.gameObject.GetComponent<PlayerResource> ().TakeDamage (damage);
		}
		Destroy (gameObject);
	}
}
