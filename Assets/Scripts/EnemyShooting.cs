using UnityEngine;
using System.Collections;

public class EnemyShooting : MonoBehaviour {
	public GameObject shot;
	public float fireRate;

	GameObject shot_spawn;
	float nextFire, dist;
	Vector3 offset= new Vector3 (0, 0.5f, 0.5f);

	void Start ()
	{
		shot_spawn = new GameObject ("shot_spawn");
		shot_spawn.transform.parent = transform;
		shot_spawn.transform.position = transform.position + offset;
	}

	void Update ()
	{
		dist = Vector3.Distance(GameObject.FindGameObjectWithTag ("Player").transform.position, transform.position);
		if(dist < 5)
		{
			if (Time.time > nextFire) {
				nextFire = Time.time + fireRate;
				Instantiate (shot, shot_spawn.transform.position, shot_spawn.transform.rotation);
			}
		}
	}
}
