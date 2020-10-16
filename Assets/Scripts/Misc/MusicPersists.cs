using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MusicPersists : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad (this);
		GameObject[] music = GameObject.FindGameObjectsWithTag("BackgroundMusic");

		for(int i = 0; i < music.Length; i++)
		{
			if(music[i].name != this.name)
			{
				Destroy(music[i]);

			}
			else
			{
				i++;
				while(i<music.Length)
				{
					Destroy(music[i]);
					i++;
				}
			}
		}

		
	}


}
