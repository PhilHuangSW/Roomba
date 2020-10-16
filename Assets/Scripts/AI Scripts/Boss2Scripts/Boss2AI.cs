using UnityEngine;
using System.Collections;

public class Boss2AI : MonoBehaviour {

	public Transform left;
	public Transform right;

	public string nextScene;

	public Color damageColor;
	private Color defaultColor;

	private bool goingLeft = true;

	private int hp = 5;			
	public float speed = .8f;

	private float waitTime = .1f;
	private float distance = .5f;

	private SpriteRenderer spriteRenderer;
	

	private float movement;

	// Use this for initialization
	void Start () {
	
		//Set up references
		spriteRenderer = GetComponent<SpriteRenderer>();

		//Initialize values
		transform.position = right.position;
		defaultColor = spriteRenderer.color;

		movement = 0;

		StartCoroutine("MoveCoroutine");


	}
	
	private IEnumerator MoveCoroutine()
	{
		//Wait a second before starting
		yield return new WaitForSeconds(1f);
		
		//Start moving
		while(hp > 0)
		{
			if(goingLeft)
			{
				while(Vector3.Distance(transform.position, left.position) > distance)
				{
					transform.position = Vector3.Lerp(right.position, left.position, movement);
					yield return null;
					movement+= Time.deltaTime*speed;
				}
				
				yield return new WaitForSeconds(waitTime);
				goingLeft = false;
				movement = 0;
			}
			else
			{
				while(Vector3.Distance(transform.position, right.position) > distance)
				{
					transform.position = Vector3.Lerp(left.position, right.position, movement);
					yield return null;
					movement+= Time.deltaTime*speed;
				}
				
				yield return new WaitForSeconds(waitTime);
				goingLeft = true;
				movement = 0;

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
				if(hp ==  4)
				{
					speed *= 1.2f;
				}
				if(hp == 2)
				{
					speed *= 1.2f;
				}
				if(hp == 1)
				{
					speed *= 1.2f;
				}

				StopCoroutine("BlinkCoroutine");
				spriteRenderer.color = defaultColor;
				StartCoroutine("BlinkCoroutine");
			}
	}
	
	private IEnumerator BlinkCoroutine()
	{
		float timePerBlink = 1f/5;
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
		Application.LoadLevel(nextScene);
	}
}
