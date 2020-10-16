using UnityEngine;
using System.Collections;

public class TurretTargetController : MonoBehaviour {

	public float waitTime = 1f;
	public float speed = 3f;

	private Vector2 topBound;
	private Vector2 bottomBound;
	private bool goingDown;

	private bool gamePaused = false;

	private bool destroyed;


	// Use this for initialization
	void Start () {
		topBound = transform.Find ("topBound").position;
		bottomBound = transform.Find ("bottomBound").position;
		destroyed = false;
		goingDown = true;
		StartCoroutine ("MovementCoroutine");
	}


	IEnumerator MovementCoroutine()
	{
		while (!destroyed || !gamePaused) 
		{
			if(goingDown)
			{
				if(transform.position.y >= bottomBound.y)
				{
					transform.position = new Vector2(transform.position.x, transform.position.y-(speed*Time.deltaTime));

				}
				else
				{
					yield return new WaitForSeconds(waitTime);
					goingDown = false;

				}
			}
			else
			{
				if(transform.position.y <= topBound.y)
				{
					transform.position = new Vector2(transform.position.x, transform.position.y+(speed*Time.deltaTime));

				}
				else
				{
					yield return new WaitForSeconds(waitTime);
					goingDown = true;
					
				}
			}
			yield return null;
		}

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
