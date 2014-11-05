using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	Rigidbody rigid;
	CubeGenerator cubeGen;
	Transform tf;
	public float speed = 7.0f;
	public GameObject remover;
	public float removerRadius;
	private float maxspeed;
	// Use this for initialization
	void Start () {
		tf = GetComponent<Transform>();
		rigid = GetComponent<Rigidbody>();
		cubeGen = GameObject.Find ("CubeGen").GetComponent<CubeGenerator>();
		maxspeed = speed;
	}
	// Update is called once per frame
	//@TODO: Add checks x = 1 in size, add start pos for player. Make a platform that looks better.
	void Update () {
		tf.rotation = Quaternion.identity;
		if(Master.done) 
		{
			if(Master.target >= Master.PONRMax)
			{
				rigidbody.velocity = new Vector3(0, 0, rigidbody.velocity.z * 1.05f);
			}  
			return;
		}
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
			gameObject.rigidbody.AddForce(0,3f + 3f * Master.speed,0,ForceMode.VelocityChange);		
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
