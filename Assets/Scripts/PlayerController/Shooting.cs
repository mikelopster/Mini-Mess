using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour
{
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;

	private float nextFire;
	Animator anim;

	void Awake()
	{
		anim = GetComponent<Animator> ();
	}

	void Update ()
	{
		if (Input.GetButton("Fire1") && Time.time > nextFire)
		{
			anim.SetBool ("shooting", true);
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		}
		else if(!Input.GetButton("Fire1"))
			anim.SetBool ("shooting", false);
	}
}