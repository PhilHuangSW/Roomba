using UnityEngine;
using System.Collections;

public class LevelSelector : MonoBehaviour 
{


	public GUISkin skin;
	
	private float width = 1920f;  
	private float height = 1080f;  
	private Vector3 scale;
	
	private void OnGUI()
	{
		scale.x = Screen.width/width;
		scale.y = Screen.height/height; 
		scale.z = 1;
		
		Matrix4x4 matrix = GUI.matrix;
		
		GUI.skin = skin;
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);
		
		if(GUI.Button(new Rect(650, 50, 400, 100), "Level 1"))
		{
			Application.LoadLevel("Level1");
		}
		
		if(GUI.Button (new Rect(650, 200, 400, 100), "Boss 1"))
		{
			Application.LoadLevel("Boss1Loader");
		}
		if(GUI.Button (new Rect(650, 350, 400, 100), "Level 2"))
		{
			Application.LoadLevel("Level2");
		}
		if(GUI.Button (new Rect(650, 500, 400, 100), "Boss 2"))
		{
			Application.LoadLevel("Boss2Loader");
		}
		if(GUI.Button (new Rect(650, 650, 400, 100), "Level 3"))
		{
			Application.LoadLevel("Level3");
		}
		if(GUI.Button (new Rect(650, 800, 400, 100), "Boss 3"))
		{
			Application.LoadLevel("Boss3Cutscene");
		}
		if(GUI.Button (new Rect(650, 950, 400, 100), "Secret"))
		{
			Application.LoadLevel("FlappyBotCutscene");
		}
		
		/*
		if(GUI.Button (new Rect(850, 1000, 177, 72), "Levels"))
		{
			Application.LoadLevel("LevelSelector");
		}
		*/
		
		GUI.matrix = matrix;
	}
}
