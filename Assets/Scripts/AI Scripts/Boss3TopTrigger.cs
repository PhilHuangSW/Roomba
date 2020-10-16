using UnityEngine;
using System.Collections;

public class Boss3TopTrigger : MonoBehaviour {
	
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
				float sign = 1f;
				if(other.rigidbody2D.velocity.x >= 0)
				{
					sign = 1f;
				}
				else
				{
					sign = -1f;
				}
				other.rigidbody2D.velocity = new Vector2(0, 0);
				other.gameObject.GetComponent<PlayerController>().stopHover();
				other.rigidbody2D.AddForce(new Vector2(sign*1500,1500));
				AIController.SubtractHP(1);
			}
			
			
		}
	}
}