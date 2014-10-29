using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	Transform trans;
	CubeGenerator cubeGen;
	public float speed = 7.0f;
	private float maxspeed;
	// Use this for initialization
	void Start () {
		trans = GetComponent<Transform>();
		cubeGen = GameObject.Find ("CubeGen").GetComponent<CubeGenerator>();
		maxspeed = speed;
		trans.position = new Vector3 (1.12f, -1.7f, 25f);
	}
	// Update is called once per frame
	//@TODO: Add checks x = 1 in size, add start pos for player. Make a platform that looks better.
	void Update () {
		trans.rotation = new Quaternion (0, 0, 0, 0);
		speed = Master.speed * maxspeed;
		if (Input.GetKeyDown(KeyCode.D)) {
			trans.Translate(1f + cubeGen.OffsetFactor,0,0);		
		}
		if (Input.GetKeyDown(KeyCode.A)) {
			trans.Translate(-(1f+cubeGen.OffsetFactor),0,0);		
		}
		if (Input.GetKeyDown(KeyCode.Space)) {
			gameObject.rigidbody.AddForce(0,3,0,ForceMode.VelocityChange);		
		}
		trans.Translate (0f, 0f, speed*Time.deltaTime);
		if (Vector3.Distance (trans.position, cubeGen.gameObject.transform.position) > 25) {
			cubeGen.Extend();		
		}
	
	}
}
