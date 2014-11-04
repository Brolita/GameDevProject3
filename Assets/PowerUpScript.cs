using UnityEngine;
using System.Collections;

public class PowerUpScript : MonoBehaviour 
{
	public float effectLength;
	public float strength;
	public float attraction;
	public float angularSpeed;
	public float angularAcceleraton;
	public GameObject PlayerObject;

	private Vector3 unitForAccel;
	private int frame;
	void Start () 
	{
		rigidbody.angularVelocity = Random.onUnitSphere * angularSpeed;
		unitForAccel = Random.onUnitSphere;
		frame = 0;
	}
	void Update () 
	{
		if (frame == 60) {
			unitForAccel = Random.onUnitSphere;
			frame = 0;
		}
		rigidbody.angularVelocity += angularAcceleraton * unitForAccel;
		rigidbody.AddForce (forceTowardsPlayer ());
		if(PlayerObject.transform.position.z > transform.position.z + 4)
		{
			Destroy(gameObject);
		}
		frame ++;
	}
	Vector3 forceTowardsPlayer() 
	{
		float magnitude = attraction * (Master.ratio () * Master.ratio () - Master.ratio () + .25f); // full magnitude at 0 and 1
		Vector3 force = magnitude * Vector3.Normalize (transform.position - PlayerObject.transform.position);
		return new Vector3 (force.x, 0, force.z); // get rid of y component

	}
}