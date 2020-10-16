using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	//Should the camera be constrained in any direction?
	private bool constrainUp = false;
	private bool constrainDown = true;
	private bool constrainLeft = false;
	private bool constrainRight = false;

	//Values to constrain the camera
	private float upConstraint = 0;
	private float downConstraint = 0;
	private float leftConstraint = 0;
	private float rightConstraint = 0;

	//Camera acceleration to follow target
	private float acceleration = 100f;

	//Offset the camera from the target transform
	private float xOffset = 0f;
	private float yOffset = 0f;

	//Target x and y
	private float targetX;
	private float targetY;
	
	
	public Transform player;		// Reference to the player's transform.
	
	void FixedUpdate ()
	{
		TrackPlayer();
	}
	
	
	void TrackPlayer ()
	{
		targetX = MiscTools.IncrementTowards(transform.position.x, player.position.x + xOffset, acceleration);
		targetY = MiscTools.IncrementTowards(transform.position.y, player.position.y + yOffset, acceleration);

		CheckConstraints();

		transform.position = new Vector3(targetX, targetY, -10);
	}
	

	private void CheckConstraints()
	{	
		//Up
		if(constrainUp && targetY > upConstraint)
		{
			targetY = upConstraint;
		}
		//Down
		if(constrainDown && targetY < downConstraint)
		{
			targetY = downConstraint;
		}
		//Left
		if(constrainLeft && targetX < leftConstraint)
		{
			targetY = leftConstraint;
		}
		//Right
		if(constrainRight && targetX > rightConstraint)
		{
			targetY = rightConstraint;
		}
	}
}
