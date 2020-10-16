using UnityEngine;
using System.Collections;

public class AIRightTrigger : MonoBehaviour {

	private PlayerController player;		//A reference to the player object that will collide with the trigger
	
	public float knockbackAmount = 700;	//Amount to knock the player back if they collide with the trigger
	public int damage = 1;					//Amount of hp to remove from the player if they collide with the trigger
	bool canEnterAgain;

	[HideInInspector]
	public bool beingHit = false;

	//If a rigidbody2D collides with the trigger
	void OnTriggerEnter2D(Collider2D other)
	{
		//If the collider2d has a player tag
		if(other.tag == "Player")
		{
			
			//Create a reference to the player
			player = other.gameObject.GetComponent<PlayerController>();
			
			//Set the x velocity to 0
			player.rigidbody2D.velocity = new Vector2(0, player.rigidbody2D.velocity.y);
			
			//Add a knockback force
			player.rigidbody2D.AddForce(new Vector2(knockbackAmount,0));
			
			
			//Subtract 1 HP
			player.SubtractHP(damage);
			beingHit = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		beingHit = false;
	}

}
