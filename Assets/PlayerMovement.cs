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
	}
	// Update is called once per frame
	//@TODO: Add checks x = 1 in size, add start pos for player. Make a platform that looks better.
	void Update () {
		speed = Master.speed * maxspeed;
		if (Input.GetKeyDown(KeyCode.D)) {
			trans.Translate(1f + cubeGen.OffsetFactor,0,0);		
		}
		if (Input.GetKeyDown(KeyCode.A)) {
			trans.Translate(-(1f+cubeGen.OffsetFactor),0,0);		
		}
		trans.Translate (0f, 0f, speed*Time.deltaTime);
		if (Vector3.Distance (trans.position, cubeGen.gameObject.transform.position) > 25) {
			cubeGen.Extend();		
		}
	
	}
}
