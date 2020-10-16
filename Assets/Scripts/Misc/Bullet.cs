using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public GameObject particles;
	private PlayerController player;


	// Use this for initialization
	void Start () {
	
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == "Player")
		{
			//Create a reference to the player
			player = other.gameObject.GetComponent<PlayerController>();
			player.SubtractHP(1);
			player.rigidbody2D.AddForce(other.relativeVelocity * 20);
			Destroy(this.gameObject);
		}

		GameObject clone = (GameObject) Instantiate(particles, transform.position, Quaternion.identity);
		Destroy(clone, .2f);
		Destroy(this.gameObject);
	}
	
}
