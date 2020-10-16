using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

	public GUISkin skin;
	public Texture2D restartTexture;
	public Texture2D quitTexture;
	
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
		
		if(GUI.Button(new Rect(800, 600, 313, 64), restartTexture))
		{
			Application.LoadLevel("StartScreen");
		}
		
		if(GUI.Button (new Rect(870, 700, 177, 72), quitTexture))
		{
			Application.Quit();
		}
		
		GUI.matrix = matrix;
	}
}
