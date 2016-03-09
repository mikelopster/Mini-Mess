using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour
{
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;

	private float nextFire;

	void Update ()
	{
		if (Input.GetButton("Fire1") && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		}
	}
}