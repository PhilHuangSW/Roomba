using UnityEngine;
using System.Collections;

public class Boss1AI : MonoBehaviour {


	//Transforms --boss stars at top right
	public Transform topLeft;
	public Transform topRight;
	public Transform bottomLeft;
	public Transform bottomRight;

	public Color damageColor;

	private bool movingClockwise = false;

	private bool goingToTopLeft = false;
	private bool goingToTopRight = true;
	private bool goingToBottomRight = false;
	private bool goingToBottomLeft = false;

	private int hp = 5;			
	private float speed = 1f;		

	private float waitTime = .1f;
	private float distance = .5f;

	private SpriteRenderer spriteRenderer;
	Color defaultColor;
	
	private float movement = 0f;
	private float upDownModifier = 2f;


	// Use this for initialization
	void Start () {

		//Set up references
		spriteRenderer = GetComponent<SpriteRenderer>();
		defaultColor = spriteRenderer.color;

		transform.position = topRight.position;
		StartCoroutine("MoveCoroutine");
	}
	
	private IEnumerator MoveCoroutine()
	{
		//Wait a second before starting
		yield return new WaitForSeconds(1f);
	
		//Start moving
		while(hp > 0)
		{
			if(movingClockwise)
			{
				if(goingToTopRight)
				{
					while(Vector3.Distance(transform.position, topRight.position) > distance)
					{
						transform.position = Vector3.Lerp(transform.position, topRight.position, speed*Time.deltaTime);
						yield return null;
					}

					//yield return new WaitForSeconds(waitTime);
					goingToTopRight = false;
					goingToBottomRight = true;

				}
				else if(goingToTopLeft)
				{
					while(Vector3.Distance(transform.position, topLeft.position) > distance)
					{
						transform.position = Vector3.Lerp(bottomLeft.position, topLeft.position, movement);
						yield return null;
						movement += speed*Time.deltaTime*upDownModifier;
					}

					yield return new WaitForSeconds(waitTime);
					goingToTopLeft = false;
					goingToTopRight = true;
					movement = 0;

				}
				else if(goingToBottomRight)
				{
					while(Vector3.Distance(transform.position, bottomRight.position) > distance)
					{
						transform.position = Vector3.Lerp(topRight.position, bottomRight.position, movement);
						yield return null;
						movement += speed*Time.deltaTime*upDownModifier;
					}

					yield return new WaitForSeconds(waitTime);
					goingToBottomRight = false;
					goingToBottomLeft = true;
					movement = 0;

				}
				else if(goingToBottomLeft)
				{
					while(Vector3.Distance(transform.position, bottomLeft.position) > distance)
					{
						transform.position = Vector3.Lerp(transform.position, bottomLeft.position, speed*Time.deltaTime);
						yield return null;
					}
					//yield return new WaitForSeconds(waitTime);
					goingToBottomLeft = false;
					goingToTopLeft = true;

				}
			}
			else
			{
				if(goingToTopRight)
				{
					while(Vector3.Distance(transform.position, topRight.position) > distance)
					{
						transform.position = Vector3.Lerp(bottomRight.position, topRight.position, movement);
						yield return null;
						movement += speed*Time.deltaTime*upDownModifier;
					}

					yield return new WaitForSeconds(waitTime);
					goingToTopRight = false;
					goingToTopLeft = true;
					movement = 0;

				}
				else if(goingToTopLeft)
				{
					while(Vector3.Distance(transform.position, topLeft.position) > distance)
					{
						transform.position = Vector3.Lerp(transform.position, topLeft.position, speed*Time.deltaTime);
						yield return null;
					}

					//yield return new WaitForSeconds(waitTime);
					goingToTopLeft = false;
					goingToBottomLeft = true;

				}
				else if(goingToBottomRight)
				{
					while(Vector3.Distance(transform.position, bottomRight.position) > distance)
					{
						transform.position = Vector3.Lerp(transform.position, bottomRight.position, speed*Time.deltaTime);
						yield return null;
					}

					//yield return new WaitForSeconds(waitTime);
					goingToBottomRight = false;
					goingToTopRight = true;

				}
				else if(goingToBottomLeft)
				{
					while(Vector3.Distance(transform.position, bottomLeft.position) > distance)
					{
						transform.position = Vector3.Lerp(topLeft.position, bottomLeft.position, movement);
						yield return null;
						movement += speed*Time.deltaTime*upDownModifier;
					}

					yield return new WaitForSeconds(waitTime);
					goingToBottomLeft = false;
					goingToBottomRight = true;
					movement = 0;

				}
			}

		}
	}

	public void SubtractHP(int amount)
	{
	
		hp -= amount;
		if (hp <= 0) 
		{
			Destroy();
		}
		else
		{
			if(hp == 4)
			{
				movingClockwise = true;
			}
			if(hp ==  2)
			{
				speed = 1.6f;
				movingClockwise = false;
			}
			if(hp == 1)
			{
				speed = 2f;
				movingClockwise = true;
			}
			spriteRenderer.color = defaultColor;
			StartCoroutine("BlinkCoroutine");
		}
		
	}

	private IEnumerator BlinkCoroutine()
	{
		float timePerBlink = .3f;
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
	

	private void Destroy()
	{
		gameObject.AddComponent<Rigidbody2D>();
		StartCoroutine("WinCoroutine");
	}

	private IEnumerator WinCoroutine()
	{
		yield return new WaitForSeconds(2f);
		Application.LoadLevel("Boss1DefeatedCutscene");
	}
}
