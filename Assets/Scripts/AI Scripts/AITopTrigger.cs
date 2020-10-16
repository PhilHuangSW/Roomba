using UnityEngine;
using System.Collections;

public class AITopTrigger : MonoBehaviour {

	public AILeftTrigger l;
	public AIRightTrigger r;

	private WalkingAI AIController;

	void Start()
	{
		AIController = transform.parent.GetComponent<WalkingAI> ();
	}


	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			if(!l.beingHit && !r.beingHit)
			{
				if(other.rigidbody2D.velocity.y < 0)
				{
					other.rigidbody2D.velocity = new Vector2(other.rigidbody2D.velocity.x, 0);
					other.gameObject.GetComponent<PlayerController>().stopHover();
					other.rigidbody2D.AddForce(new Vector2(0,1000));
					AIController.SubtractHP(1);
				}
			}

		}
	}
}
