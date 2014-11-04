using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	Rigidbody rigid;
	CubeGenerator cubeGen;
	public float speed = 7.0f;
	public GameObject remover;
	public float removerRadius;
	private float maxspeed;
	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody>();
		cubeGen = GameObject.Find ("CubeGen").GetComponent<CubeGenerator>();
		maxspeed = speed;
	}
	// Update is called once per frame
	//@TODO: Add checks x = 1 in size, add start pos for player. Make a platform that looks better.
	void Update () {
		speed = Master.speed * maxspeed;
		if (Input.GetKey(KeyCode.D)) {
			rigid.velocity = new Vector3(6f * Master.speed,rigid.velocity.y,speed);		
		}
		else if (Input.GetKey(KeyCode.A)) {
			rigid.velocity = new Vector3(-6f * Master.speed,rigid.velocity.y,speed);			
		}
		else {
			rigid.velocity = new Vector3(0,rigid.velocity.y,speed);			
		}
		if (Input.GetKeyDown(KeyCode.Space) && transform.position.y < .01f) {
			gameObject.rigidbody.AddForce(0,3f + 6f * Master.speed,0,ForceMode.VelocityChange);		
		}
		if (Vector3.Distance (transform.position, cubeGen.gameObject.transform.position) > 25) {
			cubeGen.Extend();		
		}
	
	}

	void OnTriggerEnter(Collider coll) {
		if(coll.tag == "Spike")
		{
			Master.hit ();
			GameObject r = (GameObject)Instantiate (remover, transform.position, transform.rotation);
			r.GetComponent<SphereCollider>().radius = removerRadius;
		}
		else if (coll.tag == "Drug") 
		{
			Master.effectLength += coll.GetComponent<PowerUpScript>().effectLength;
			Master.target += coll.GetComponent<PowerUpScript>().strength;
			Destroy(coll.gameObject);
		}
	}

}
