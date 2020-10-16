using UnityEngine;
using System.Collections;

public class Boss2TopTrigger : MonoBehaviour {

	private Boss2AI AIController;
	
	void Start()
	{
		AIController = transform.parent.GetComponent<Boss2AI> ();
	}
	
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player")
		{

			if(other.rigidbody2D.velocity.y < 0)
			{
				other.rigidbody2D.velocity = new Vector2(other.rigidbody2D.velocity.x, 0);
				other.gameObject.GetComponent<PlayerController>().stopHover();
				other.rigidbody2D.AddForce(new Vector2(0,3000));
				AIController.SubtractHP(1);
			}

			
		}
	}
}
