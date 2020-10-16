using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public delegate void KeyDown();
	public static event KeyDown EscPressed;
	
	private bool allowControl;


	// Use this for initialization
	void Start () {
		allowControl = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Pause"))
		{
			if(EscPressed != null)
				EscPressed();

		}
	}

	public bool CanControl()
	{
		return allowControl;
	}

	public static void PauseGame()
	{
		if(EscPressed != null)
			EscPressed();
	}
}
