using UnityEngine;
using System.Collections;

public class MiscTools{

	//Increment n towards target by acc
	public static float IncrementTowards(float n, float target, float acc)
	{
		if(n == target)
		{
			return n;	
		}
		else 
		{
			float direction = Mathf.Sign(target-n); //Direction to move n (positive or negative)
			n += acc * Time.deltaTime * direction;
			return (direction == Mathf.Sign(target-n))? n: target; //If the direction is the same now as it was, before incrementing, return n. 
			//If it has changed (gone too far), return the target.
		}
	}

}
