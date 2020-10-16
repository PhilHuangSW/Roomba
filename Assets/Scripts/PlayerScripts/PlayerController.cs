using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Color damageColor;

	private GameManager gameManager;		//A reference to the game manager

	private bool facingRight = true;		//Which way the player is currently facing.
	private bool gamePaused = false;

	//Movement parameters
	private float walkForce = 140f;
	private float runForce = 100f;
	private float maxWalkSpeed = 8f;	
	private float maxRunSpeed = 15f;
	private float deceleration = 60f;
	private float jumpForce = 400f;		//Amount of force added when the player jumps.
	
	private Transform groundCheck;			//A position marking where to check if the player is grounded.
	private Transform landCheck;			//A position marking where to check if the player is about to land.
	private bool grounded = false;			//Whether or not the player is grounded.

	//I am using a "newGrounded" temporary variable to see if there was a change from not being grounded to becoming gronded, so 
	//that I can trigger the Jump variable in the animator.
	private bool newGrounded = false;
	
	private Animator animator;			//The animator to control player animation
	
	private float h;                        //Horizontal input

	private float extraJump;				//The amount of force to add if the jump key is held
	private bool continuousJump;			//A boolean to check whether the jump key has been held continuously
	private bool jumpReleased = true;		//A boolean to check wheter or not the player has released jump while grounded so that they can jump again.

	private const float extraJumpAmount = 200f;			//The starting amount of force to add if the jump key is held
	private const float extraJumpDecrementAmount = 20f;	//The amount of force to decrement every frame the jump key is being held

	private int hp;						//The amount of hitpoints the player has
	private bool isAlive;

	private float invulnerableTime = 1f;	//The time the player is invulnerable after being hit
	private bool canBeHit;

	//Extra Abilities
	//Control booleans
	public bool canHover = true;

	//Control for forcing roomba to stop hovering (in case of adding force)
	private bool cantHover = false;

	//Hover
	private float hoverTime = 1f;		//Amount of time the player can hover for
	private float timeHovered;			//Amount of time the player has hovered for
	private Vector2 gravityOffset = new Vector2(0, 65);		//The force to offset gravity when hovering
	private bool initialJumpReleased;	//The jump key has been released after the initial press
	private bool startedHovering;

	private ParticleSystem dustParticles;
	private LivesCounter livesCounter;

	private Vector2 currentCheckpoint = Vector2.zero;

	private SpriteRenderer spriteRenderer;
	Color defaultColor;

	void Awake()
	{
		// Setting up references.
		groundCheck = transform.Find("groundCheck");
		landCheck = transform.Find ("landCheck");
		gameManager = GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<GameManager>();
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		dustParticles = transform.Find("DustParticles").GetComponent<ParticleSystem>();
		livesCounter = GameObject.FindGameObjectsWithTag("LivesCounter")[0].GetComponent<LivesCounter>();



		//Setting initial values
		continuousJump = false;
		extraJump = extraJumpAmount;
		hp = 10;
		isAlive = true;
		canBeHit = true;
		timeHovered = 0f;
		startedHovering = false;
		animator.Play ("RoombaIdle");
		animator.SetBool("Hover", false);
		animator.SetBool("Jump", false);

		defaultColor = spriteRenderer.color;





	}


	void FixedUpdate () {
		if(gameManager.CanControl() && isAlive && !gamePaused)
		{
			Jump();
			HorizontalMovement();
		}
	}

	private void HorizontalMovement()
	{
		//Horizontal Input
		h = Input.GetAxisRaw("Horizontal");
		
		//If there is horizontal input, change the velocity
		if(h != 0)
		{
			//If Run input is detected, run
			if(Input.GetAxisRaw("Run") == 1)
			{
				//animator.SetFloat("Speed", Mathf.Abs(h)+1);
				//Set the velocity. Multiply by h to set direction
				rigidbody2D.velocity = new Vector2(MiscTools.IncrementTowards(rigidbody2D.velocity.x, h*maxRunSpeed, runForce), rigidbody2D.velocity.y);
				
				
			}
			//If no run input is detected, walk
			else
			{
				//animator.SetFloat("Speed", Mathf.Abs(h));
				//Set the velocity. Multiply by h to set direction
				rigidbody2D.velocity = new Vector2(MiscTools.IncrementTowards(rigidbody2D.velocity.x, h*maxWalkSpeed, walkForce), rigidbody2D.velocity.y);
			}
			//Emit particles
			if(grounded)
			{
				dustParticles.enableEmission = true;
			}
			else
			{
				dustParticles.enableEmission = false;
			}
		}
		else
		{
			dustParticles.enableEmission = false;
		}
		
		
		// If the input is moving the player right and the player is facing left...
		if(h > 0 && !facingRight)
			Flip();
		// Otherwise if the input is moving the player left and the player is facing right...
		else if(h < 0 && facingRight)
			Flip();
		

		//If the player is grounded and there is no more horizontal input, decelerate the player
		if(!Input.GetButton("Horizontal") && grounded)
		{
			rigidbody2D.velocity = new Vector2(MiscTools.IncrementTowards(rigidbody2D.velocity.x, 0, deceleration), rigidbody2D.velocity.y);
			//animator.SetFloat("Speed", 0);
		}

		//If the player's y position gets lower than a certain point, respawn and subtract 1 hp
		if (transform.position.y < -30) 
		{
			transform.position = currentCheckpoint;
			SubtractHP(1);
		}

	}

	private void Jump()
	{
		//if the y velocity is <= 0 then the player can hit the ground, so check it
		if(rigidbody2D.velocity.y <= 0)
		{
			// The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
			newGrounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
			if(Physics2D.Linecast(transform.position, landCheck.position, 1 << LayerMask.NameToLayer("Ground")))
			{
				animator.SetBool("Jump", false);
			}

			  

			if(!grounded && newGrounded)
			{
				extraJump = extraJumpAmount;

				//Reset parameters when the ground is hit.
				initialJumpReleased = true;

				timeHovered = 0;
				startedHovering = false;
				cantHover=false;
			}

			grounded = newGrounded;

		}
		
		
		
		// If the jump button is pressed
		if(Input.GetButton("Jump"))
		{
			
			//If the player is grounded and the jump button was released, add the initial force
			if(grounded && jumpReleased)
			{
				initialJumpReleased = false;
				animator.SetBool("Jump", true);
				// Add a vertical force to the player.
				rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 1);
				rigidbody2D.AddForce(new Vector2(0, jumpForce));
				grounded = false;
				continuousJump = true;
				jumpReleased = false;

			}
			//Else if the player is not grounded, if the press was continuous then add more force and subtract the force to be added in the next cycle
			else if(continuousJump)
			{
				//If there is still some extra jump amount to be added
				if(extraJump > 0)
				{
					rigidbody2D.AddForce(new Vector2(0, extraJump));
					extraJump -= extraJumpDecrementAmount;
				}
				else
				{
					continuousJump = false;

				}
			}
		}
		//If the jump button is not pressed, then the chain of presses has ended, so set continuousJump to false.
		else
		{
			continuousJump = false;
		}

		if (!Input.GetButton("Jump")) 
		{
			initialJumpReleased = true;
		}

		//If the player can hover, they aren't in a continuous jump, they aren't grounded , 
		//they have released the jump key after the initial press, and they press the spacebar again, start to hover
		if (canHover && !continuousJump && !grounded && initialJumpReleased && Input.GetButton ("Jump") && !cantHover) 
		{
			startedHovering = true;
			if (timeHovered < hoverTime) 
			{
				animator.SetBool("Hover", true);
				//Set the y velocity to zero
				rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, 0);
				//Add a force to offset gravity
				rigidbody2D.AddForce (gravityOffset);
			}
			else
			{
				animator.SetBool("Hover", false);
			}
		}
		else
		{
			animator.SetBool("Hover", true);
		}

		//If the player is hovering, add to the time they have hovered
		if (startedHovering) 
		{
			timeHovered += Time.deltaTime;
		}

		//If the player is grounded and Jump isn't pressed, set jumpReleased to true so that another jump can be performed
		//(this is here so that the player won't automatically jump again when they hit the ground if they are holding space from a previous jump)
		if(grounded && !Input.GetButton("Jump"))
		{
			jumpReleased = true;
		}
	}

	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		if(facingRight)
		{
			transform.Find("DustParticles").eulerAngles = new Vector3(0, 270, 0);
		}
		else
		{
			transform.Find("DustParticles").eulerAngles = new Vector3(180, 270, 0);
		}
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}


	public void SubtractHP(int amountToSubtract)
	{

		if(canBeHit && hp>0)
		{
			hp -= amountToSubtract;
			
			if(hp <= 0)
			{
				PlayerDies();
			}
			else
			{
				StopCoroutine("BlinkCoroutine");
				spriteRenderer.color = defaultColor;
				StartCoroutine("BlinkCoroutine");
			}
			StartCoroutine("InvulnerableCoroutine");
		}

	}

	private IEnumerator BlinkCoroutine()
	{
		float timePerBlink = invulnerableTime/5;

		//Switch to hit color, wait, then switch back
		spriteRenderer.color = damageColor;
		yield return new WaitForSeconds(timePerBlink);
		spriteRenderer.color = defaultColor;
		yield return new WaitForSeconds(timePerBlink);
		spriteRenderer.color = damageColor;
		yield return new WaitForSeconds(timePerBlink);
		spriteRenderer.color = defaultColor;
		yield return new WaitForSeconds(timePerBlink);
		spriteRenderer.color = damageColor;
		yield return new WaitForSeconds(timePerBlink);
		spriteRenderer.color = defaultColor;
	}

	//What happens when the player dies
	private void PlayerDies()
	{
		dustParticles.enableEmission = false;
		isAlive = false;
		rigidbody2D.fixedAngle = false;
		rigidbody2D.AddForce(new Vector2(1000, 0));
		StartCoroutine("SubtractLife");
	}

	IEnumerator SubtractLife()
	{
		//Wait for 3 seconds, then subtract a life and restart the level
		yield return new WaitForSeconds(3f);

		livesCounter.decrementLives();
		if(livesCounter.getLives() > 0)
		{
			Application.LoadLevel(Application.loadedLevel);
		}
		else
		{
			Destroy(livesCounter);
			Application.LoadLevel("GameOver");
		}
	}

	//HP property
	public int HP
	{ 
		get
		{
			return hp;	
		}
	}

	IEnumerator InvulnerableCoroutine()
	{
		//Play hit animation


		canBeHit = false;
		yield return new WaitForSeconds (invulnerableTime);
		canBeHit = true;

		//Resume normal animation

	}

	void OnEnable()
	{
		GameManager.EscPressed += PauseGame;
	}

	void OnDisable()
	{
		GameManager.EscPressed -= PauseGame;
	}

	void PauseGame()
	{
		if(!gamePaused)
		{
			gamePaused = true;
			Time.timeScale = 0;

		}
		else
		{
			gamePaused = false;
			Time.timeScale = 1;
		}
	}

	public void SetCheckpoint(Vector2 checkpoint)
	{
		currentCheckpoint = checkpoint;
	}

	public void stopHover()
	{
		cantHover = true;
	}




}
