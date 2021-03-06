﻿using UnityEngine;
using System.Collections;

public class Life : MonoBehaviour {
	public int HP = 100;
	public float upStar = 0.5f;
	public bool onFirst = false;

	IEnumerator WaitForDead()
	{
		yield return new WaitForSeconds (2);
		Destroy (gameObject);
	}

	public void Dead () {
		EnemyFollowing.instance.RemoveFollowing (transform);

		// Call Quest
		EnemyEvent evt = this.gameObject.GetComponent<EnemyEvent> ();

		if(evt != null)
			QuestManager.instance.CheckEnemyQuest (evt.enemyIndex, evt.enemyMain);

		transform.Rotate (-90, 0, 0);
		StartCoroutine(WaitForDead());
	}

	public void TakeDamage (int damage) {
		if (!onFirst) {
			EnemyShooting st = GetComponent<EnemyShooting> ();

			if (st != null)
				GetComponent<EnemyShooting> ().enabled = true;

			EnvironmentSystem.instance.RemoveEnvironmentControl (transform);
			EnemyFollowing.instance.AddFollowing (transform);

			onFirst = true;
		}


		ResourceManager.instance.SetStar (upStar);
//
		// Remove from Environment controller


		HP -= damage;
		if (HP < 0)
			Dead ();
	}

	public void Cure (int curePoint)
	{
		HP += curePoint;
	}
		
}
