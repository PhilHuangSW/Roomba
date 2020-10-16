using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	private bool gamePaused = false;

	private GameObject pauseMenu;

	public GUISkin skin;
	public Texture2D resumeTexture;
	public Texture2D mainMenuTexture;
	public Texture2D quitTexture;
	public Texture2D tintTexture;
	public Texture2D pauseWindowTexture;

	private float width = 1920f;  
	private float height = 1080f;  
	private Vector3 scale;
	
	private void OnGUI()
	{
		if(gamePaused)
		{
			scale.x = Screen.width/width;
			scale.y = Screen.height/height; 
			scale.z = 1;
			
			Matrix4x4 matrix = GUI.matrix;
			
			GUI.skin = skin;
			GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);

			GUI.DrawTexture(new Rect(0, 0, 1920, 321), pauseWindowTexture);
			
			if(GUI.Button(new Rect(150, 70, 314, 64), resumeTexture))
			{
				GameManager.PauseGame();
			}

			if(GUI.Button(new Rect(750, 70, 466, 63), mainMenuTexture))
			{
				GameManager.PauseGame();
				Application.LoadLevel("StartScreen");
			}
			
			if(GUI.Button (new Rect(1450, 70, 177, 72), quitTexture))
			{
				Application.Quit();
			}

			GUI.matrix = matrix;
		}
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

		if(gamePaused)
		{
			if(pauseMenu != null)
				Destroy(pauseMenu);
		}
		else
		{
			pauseMenu = new GameObject();
			pauseMenu.transform.position = new Vector3(0.5f, 0.5f, 0);
			pauseMenu.transform.localScale = new Vector3(1, 1, 1);
			pauseMenu.AddComponent("GUITexture");
			pauseMenu.guiTexture.texture = tintTexture;
		}
		gamePaused = !gamePaused;
	}
	
}
