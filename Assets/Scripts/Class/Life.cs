using UnityEngine;
using System.Collections;

public class Life : MonoBehaviour {
	public int HP = 100;
	public float upStar = 0.5f;

	IEnumerator WaitForDead()
	{
		yield return new WaitForSeconds (2);
		Destroy (gameObject);
	}

	public void Dead ()
	{
		EnemyFollowing.instance.RemoveFollowing (transform);

		// Call Quest
		EnemyEvent evt = this.gameObject.GetComponent<EnemyEvent> ();

		if(evt != null)
			QuestManager.instance.CheckEnemyQuest (evt.enemyIndex, evt.enemyMain);

		transform.Rotate (-90, 0, 0);
		StartCoroutine(WaitForDead());
	}

	public void TakeDamage (int damage) {
		GetComponent<EnemyShooting> ().enabled = true;
		// Add Following Player
		EnemyFollowing.instance.AddFollowing (transform);
		ResourceManager.instance.SetStar (upStar);

		// Remove from Environment controller
		EnvironmentSystem.instance.RemoveEnvironmentControl (transform);

		HP -= damage;
		if (HP < 0)
			Dead ();
	}

	public void Cure (int curePoint)
	{
		HP += curePoint;
	}
		
}
