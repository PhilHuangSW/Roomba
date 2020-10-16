using UnityEngine;
using System.Collections;

public class TurretController : MonoBehaviour {
	
	private Transform target;

	private Vector2 distance;
	private Vector3 offset = new Vector3(90,0,0);


	// Use this for initialization
	void Start () {
		target = transform.parent.Find ("target");

	}
	
	// Update is called once per frame
	void Update () {
		distance = target.position - transform.position;
		transform.localRotation = Quaternion.LookRotation (distance);
		transform.eulerAngles += offset;

	
	}


}
