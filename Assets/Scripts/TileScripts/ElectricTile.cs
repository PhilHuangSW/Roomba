using UnityEngine;
using System.Collections;

public class ElectricTile : MonoBehaviour {


	void OnCollisionStay2D(Collision2D other)
	{
		if(other.gameObject.tag == "Player")
		{
			//Create a reference to the player
			PlayerController player = other.gameObject.GetComponent<PlayerController>();
			player.SubtractHP(1);
		}
		

	}
}
