using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class IntroCutscene : MonoBehaviour {

	public GUISkin skin;
	public List<string> lines = new List<string>();

	public string nextLevelName;
	
	private float width = 1920f;  
	private float height = 1080f;  
	private Vector3 scale;

	private string currentString;
	private int lineIndex;

	void Start()
	{
		lineIndex = 0;
		if (lines.Count > 0) 
		{
			currentString = lines [lineIndex];
			lineIndex++;
		}
		else
			currentString = "";

	}

	
	private void OnGUI()
	{
		scale.x = Screen.width/width;
		scale.y = Screen.height/height; 
		scale.z = 1;
		
		Matrix4x4 matrix = GUI.matrix;
		
		GUI.skin = skin;
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);
		
		//Press escape to skip..
		GUI.Box(new Rect(450, 1000, 1400, 75), "Press Esc to skip cutscene; Space to continue");

		GUI.Box (new Rect(80, 150, 1720, 800), currentString);
		
		GUI.matrix = matrix;
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			if(lines.Count > lineIndex)
			{
				currentString = lines[lineIndex];
				lineIndex++;
			}
			else
			{
				NextScene();
			}
		}

		if(Input.GetKeyDown(KeyCode.Escape))
		{
			NextScene();
		}
	}

	void NextScene()
	{
		Application.LoadLevel (nextLevelName);
	}
}
