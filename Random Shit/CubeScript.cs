using UnityEngine;
using System.Collections;

public class CubeScript: MonoBehaviour {
	
	
	public GameObject PlayerObject;
	
	public int diff = 5;
	
	public float Speed;
	
	public GameObject Boulder;
	
	int width = 0;
	
	public float Decay = 4.0f;
	
	float elasped;
	

	
	
	CubeGenerator cg;
	

	// Use this for initialization
	void Start () {
		
		cg = GameObject.Find("CubeGen").GetComponent<CubeGenerator>();
		width = (int)cg.Size.x;
	
	}
	
	
	void OnDestroy() {
		
		if (Application.isPlaying) {
			cg.Extend();
		}
	}
	
	
	void OnCollisionEnter(Collision c)
	{
		if ( c.gameObject.tag == "d")
		{
			//c.gameObject.rigidbody.useGravity = true;
			
		}
	}
	

	// Update is called once per frame
	void FixedUpdate () {
		
		if (PlayerObject.transform.position.z > transform.position.z + diff)
		{
			gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0.2f,-1.3f,0.0f) * Random.value ,ForceMode.Impulse);
			
			
			Destroy(gameObject,Decay);
			
			
		
			
		
			
			
		}
		
		
	
	
	}
	
	
	
	
}
