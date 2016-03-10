using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResourceManager : MonoBehaviour {

	public static ResourceManager instance;
	public Text starText;

	public float star = 0;
	public float gold;
	// Use this for initialization
	void Start () {
		instance = this;
		gold = 1000f;
		star = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(star > 0)
			star -= 0.005f * Time.deltaTime;

		if (star >= 1) {
			// 1 Star
			starText.text = "1";
			// Call Army!!
			EnemyStar.instance.SpawnArmy (1);
		} else if(star < 0) {
			EnemyStar.instance.ClearArmy ();
		}

		Debug.Log ("Star: " + star);
	}

	public void SetStar(float set_star) {
		star += set_star;
	}

	public float GetStar() {
		return star;
	}

}
