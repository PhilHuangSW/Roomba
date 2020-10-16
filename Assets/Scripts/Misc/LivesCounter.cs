using UnityEngine;
using System.Collections;

public class LivesCounter : MonoBehaviour {

	private int lives = 5;

	
	void Start()
	{
		DontDestroyOnLoad(this);
	}

	public int getLives()
	{
		return lives;
	}

	public void decrementLives()
	{
		lives--;
	}

}
