using UnityEngine;
using System.Collections;

public class GUITextController : MonoBehaviour {

	public Font font;
	public GUISkin skin;

	private PlayerController player;
	private LivesCounter livesCounter;
	
	private float width = 1920f;  
	private float height = 1080f;  
	private Vector3 scale;

	private bool gamePaused = false;
	private GameObject textObject;


	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerController>();
		livesCounter = GameObject.FindGameObjectsWithTag("LivesCounter")[0].GetComponent<LivesCounter>();
	}

	private void OnGUI()
	{
		if(!gamePaused)
		{
			scale.x = Screen.width/width;
			scale.y = Screen.height/height; 
			scale.z = 1;
			
			Matrix4x4 matrix = GUI.matrix;
			
			GUI.skin = skin;
			GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);
			
			GUI.Box(new Rect(0, 0, 300, 80), "Health: " + player.HP);
			GUI.Box(new Rect(1700, 0, 220, 80), "Lives: " + livesCounter.getLives());

			
			GUI.matrix = matrix;
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