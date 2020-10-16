using UnityEngine;
using System.Collections;

public class Boss1TopTrigger : MonoBehaviour {
	

	private Boss1AI AIController;
	
	void Start()
	{
		AIController = transform.parent.GetComponent<Boss1AI> ();
	}
	
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player")
		{

			if(other.rigidbody2D.velocity.y < 0)
			{
				other.rigidbody2D.velocity = new Vector2(other.rigidbody2D.velocity.x, 0);
				other.rigidbody2D.AddForce(new Vector2(0,1400));
				AIController.SubtractHP(1);
			}

			
		}
	}
}
