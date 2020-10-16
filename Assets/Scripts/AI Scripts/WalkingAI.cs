using UnityEngine;
using System.Collections;

public class WalkingAI : MonoBehaviour {


	public int hp = 1;					//Enemy hit point
	public float speed = 1f;			//Enemy movement speed (0 = no movement)
	public float jumpHeight = 0f;		//Enemy jump height (0 = no jump)
	//public int attack = 0;				//Enemy attack type (0 = wander only, 1 = run towards player, 2 = shoot etc)

	public bool defeatingLoadsLevel = false;
	public string levelToLoad;
	
	public bool switchOnBounds = false;		//Bool selecting whether the A.I. will switch direction after a certain time
	public bool useTimer = true;		//Bool selecting whether the A.I. will switch direction when a boundary is reached
	public bool stopOnBounds = true;	//Bool selecting wheter the A.I. will stop movement at the bounds until the direction is switched

	public float switchTimer = 5f;

	private float velocity = 5f;		//The movement velocity
		
	private Vector2 leftBound;			//Left boundry for the A.I.
	private Vector2 rightBound;			//Right boundry for the A.I.

	private bool moveRight = false;		//Should the A.I. be moving right?

	private float jumpForce = 2000f;	//Jump force

	public float jumpTimer = 3f;		//Time the player waits until jumping
	private Transform groundCheck;		//A position marking where to check if the A.I. is grounded.

	private bool gamePaused = false;

	private bool dead = false;


	// Use this for initialization
	void Start () {
		//Get the boundry components
		Transform t;
		t = transform.Find("leftBound");
		leftBound = new Vector2(t.position.x, t.position.y);
		t = transform.Find("rightBound");
		rightBound = new Vector2(t.position.x, t.position.y);

		groundCheck = transform.Find("groundCheck");

		if (useTimer) 
		{
			StartCoroutine ("TimerCoroutine");
		}
		if (jumpHeight != 0) 
		{
			StartCoroutine ("JumpCoroutine");
		}
	}
	
	// Update is called once per frame
	void Update () {

		//If the A.I. shouldn't be chasing the player
		if(!dead && !gamePaused)
		{
			MoveInLoop();
		}
	}

	//Move the A.I. Between the two bounds
	void MoveInLoop()
	{

		//If the A.I. should be moving right
		if(moveRight)
		{
			//If the A.I.'s position.x is less than rightBound.x, or we don't want to stop at the bounds
			if(transform.position.x < rightBound.x || !stopOnBounds)
			{
				//Set the velocity to the right
				rigidbody2D.velocity = new Vector2(velocity * speed, rigidbody2D.velocity.y);
			}
			//Else start moving left
			else
			{
				transform.position = new Vector2(rightBound.x, transform.position.y);
				if(switchOnBounds)
				{
					moveRight = false;
					Flip();
				}
			}
		}
		//Else if it should be moving left
		else
		{
			//If the A.I.'s position.x is greater than or equal to leftBound.x, or we don't want to stop at the bounds
			if(transform.position.x >= leftBound.x  || !stopOnBounds)
			{
				//Set the velocity to the left
				rigidbody2D.velocity = new Vector2(-1*velocity * speed, rigidbody2D.velocity.y);
			}
			//Else start moving right
			else
			{
				transform.position = new Vector2(leftBound.x, transform.position.y);
				if(switchOnBounds)
				{
					moveRight = true;
					Flip();
				}
			}
		}
	}

	void Flip ()
	{
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}


	IEnumerator TimerCoroutine()
	{
		if(!gamePaused)
		{
			//Wait for the timer
			yield return new WaitForSeconds (switchTimer);
			//Alternate the direction
			moveRight = !moveRight;
			//Restart the coroutine
			if (useTimer) 
			{
				StartCoroutine ("TimerCoroutine");
			}
		}
		else
		{
			yield return null;
		}
	}

	IEnumerator JumpCoroutine()
	{
		if(!gamePaused)
		{
			//Wait for the jump timer amount
			yield return new WaitForSeconds(jumpTimer);
			//If grounded, jump
			if(Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")))
			{
				rigidbody2D.AddForce(new Vector2(0, jumpForce * jumpHeight));
			}
			//Restart the coroutine
			if (jumpHeight != 0) 
			{
				StartCoroutine ("JumpCoroutine");
			}
		}
		else
		{
			yield return null;
		}

	}

	public void SubtractHP(int amount)
	{
		hp -= amount;
		if (hp <= 0) 
		{
			Destroy();
		}
	}

	public void Destroy()
	{
		dead = true;
		StopCoroutine("JumpCoroutine");
		StopCoroutine("TimerCoroutine");
		rigidbody2D.fixedAngle = false;
		rigidbody2D.AddTorque(40);
		rigidbody2D.gravityScale = .5f;
		Destroy(GetComponent("CircleCollider2D"));
		Destroy(GetComponent("BoxCollider2D"));
		if(!defeatingLoadsLevel)
		{
			Destroy (this.gameObject,2f);
		}
		else
		{
			StartCoroutine("LoadCoroutine");
		}
	}

	IEnumerator LoadCoroutine()
	{
		yield return new WaitForSeconds(2f);
		Application.LoadLevel(levelToLoad);
	}

	void OnEnable()
	{
		GameManager.EscPressed += GamePaused;
	}

	void OnDisable()
	{
		GameManager.EscPressed -= GamePaused;
	}

	void GamePaused()
	{
		gamePaused = !gamePaused;
	}
}
