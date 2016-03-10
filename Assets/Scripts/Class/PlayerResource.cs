using UnityEngine;
using System.Collections;

public class PlayerResource : MonoBehaviour {
	public int money;

	public void Paid (int price)
	{
		money -= price;
	}

	public void PickUp (int price)
	{
		money += price;
	}
}
