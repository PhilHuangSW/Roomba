using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class IconManager : MonoBehaviour {

	public GUISkin skin;
	public Texture textureRight;
	public Texture textureLeft;

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
		
		GUI.DrawTexture(new Rect(0,780, 300,300), textureRight);
		GUI.DrawTexture(new Rect(1620,780, 300,300), textureLeft);
		
		GUI.matrix = matrix;
	}
}
