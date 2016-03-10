using UnityEngine;
using System.Collections;

public class PlayerResource : MonoBehaviour {
	public int HP = 100;
	public float upStar = 0.5f;

	IEnumerator WaitForDead()
	{
		yield return new WaitForSeconds (5);
		transform.position = GameObject.FindGameObjectWithTag ("Savepoint").transform.position;
		transform.Rotate (0, 0, 0);
		HP = 100;
	}

	public void Dead ()
	{
		transform.Rotate (90, 0, 0);
		StartCoroutine(WaitForDead());
	}

	public void TakeDamage (int damage) 
	{
		if (HP > 0) 
		{
			HP -= damage;
			if (HP <= 0)
				Dead ();
		}
	}

	public void Cure (int curePoint)
	{
		HP += curePoint;
	}

}
