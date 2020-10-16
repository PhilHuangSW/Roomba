using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			PlayerController player = other.gameObject.GetComponent<PlayerController>();
			player.SetCheckpoint(transform.position);
		}
	}
}
