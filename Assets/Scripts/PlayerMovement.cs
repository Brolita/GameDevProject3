using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	Rigidbody rigid;
	CubeGenerator cubeGen;
	public float speed = 7.0f;
	public GameObject remover;
	public float removerRadius;
	private float maxspeed;
	public Animator anim;
	GameObject mainCam;
	Transform tf;
	float jmpTmr =30;
	// Use this for initialization
	void Start () {
		anim = GetComponentInChildren<Animator>();

		tf  = GetComponent<Transform>();
		mainCam = GameObject.FindGameObjectWithTag("MainCamera");
		rigid = GetComponent<Rigidbody>();
		cubeGen = GameObject.Find ("CubeGen").GetComponent<CubeGenerator>();
		maxspeed = speed;
	}
	// Update is called once per frame
	//TODO: Add checks x = 1 in size, add start pos for player. Make a platform that looks better.
	void Update () {
		jmpTmr--;
		mainCam.transform.position = new Vector3(tf.position.x,
		                                         tf.position.y+2f,
		                                         tf.position.z - 5f);
		//tf.rotation = Quaternion.identity;
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

			//Vector3 lerpPos = tf.position + Vector3.right;
			//Vector3.Lerp(tf.position,lerpPos,1f);
			rigid.velocity = new Vector3(6f * Master.speed,rigid.velocity.y,speed);		
		}
		else if (Input.GetKey(KeyCode.A)) {
			//Vector3 lerpPos = tf.position + Vector3.left;
			//Vector3.Lerp(tf.position,lerpPos,Master.ratio()*speed);
			rigid.velocity = new Vector3(-6f * Master.speed,rigid.velocity.y,speed);			
		}
		else {
			//Vector3 lerpPos = tf.position + Vector3.forward * speed;
			//Vector3.Lerp(tf.position,lerpPos,100f);
			anim.SetFloat("Value",Master.ratio());
			rigid.velocity = new Vector3(0,rigid.velocity.y,speed);			
		}
		if (Input.GetKeyDown(KeyCode.Space) && transform.position.y < .01f && jmpTmr<0) {
			anim.SetTrigger("jumpStart");
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
