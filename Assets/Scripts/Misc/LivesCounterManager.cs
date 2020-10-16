using UnityEngine;
using System.Collections;

public class LivesCounterManager : MonoBehaviour {

	public GameObject livesCounter;

	private GameObject[] livesCounters;
	// Use this for initialization
	void Start () {
		livesCounters = GameObject.FindGameObjectsWithTag("LivesCounter");
		for(int i = 0; i < livesCounters.Length; i++)
		{
			Destroy(livesCounters[i]);
		}
		Instantiate(livesCounter);
	}
	

}
