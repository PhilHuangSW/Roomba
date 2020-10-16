using UnityEngine;
using System.Collections;

public class LaserPointer : MonoBehaviour {

	private float shootSpeed = 7f;
	public GameObject bullet;
	public float waitTime = 1f;
	private Transform bulletSpawn;
	private Transform target;
	private Vector2 difference;
	private float distance;
	private Vector3 startScale;
	private Vector3 startPos;
	private BoxCollider boxCollider;
	private bool canShoot;
	private bool turretAlive;


	// Use this for initialization
	void Start () {
		startScale = transform.localScale;
		target = transform.parent.parent.Find ("target");
		bulletSpawn = transform.parent.Find ("bulletSpawn");
		startPos = transform.position;
		canShoot = true;
		turretAlive = true;
		boxCollider = (BoxCollider) gameObject.AddComponent("BoxCollider");
		boxCollider.isTrigger = true;
		StartCoroutine("LaserCoroutine");
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "RoombaTrigger")
		{
			if(canShoot)
			{

				GameObject clone = (GameObject) Instantiate(bullet, bulletSpawn.position, Quaternion.identity);
				difference = other.transform.position - startPos;
				clone.rigidbody2D.velocity = (difference * shootSpeed);
				StartCoroutine("WaitCoroutine");
			}
		}
	}

	void OnTriggerStay(Collider other)
	{
		if(other.tag == "RoombaTrigger")
		{
			if(canShoot)
			{
				
				GameObject clone = (GameObject) Instantiate(bullet, bulletSpawn.position, Quaternion.identity);
				difference = other.transform.position - startPos;
				clone.rigidbody2D.velocity = (difference * shootSpeed);
				StartCoroutine("WaitCoroutine");
			}
		}
	}

	IEnumerator LaserCoroutine()
	{
		while (turretAlive) 
		{
			startPos = transform.parent.position;
			difference = target.position - startPos;
			distance = Mathf.Sqrt (difference.x * difference.x + difference.y * difference.y);
			transform.localScale = new Vector3 (startScale.x, distance, startScale.z);
			transform.position = new Vector2 (startPos.x + difference.x / 2, startPos.y + difference.y / 2);
			yield return null;
		}
	}

	IEnumerator WaitCoroutine()
	{
		canShoot = false;
		yield return new WaitForSeconds(waitTime);
		canShoot = true;
	}
}
