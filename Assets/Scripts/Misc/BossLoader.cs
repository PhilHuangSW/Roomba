using UnityEngine;
using System.Collections;

public class BossLoader : MonoBehaviour {

	public string bossLevelName;

	void OnTriggerEnter2D(Collider2D other)
	{
		//If the collider2d has a player tag
		if(other.tag == "Player")
		{
			
			Application.LoadLevel(bossLevelName);
		}
	}
}
