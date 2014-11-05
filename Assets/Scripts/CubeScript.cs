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
	
	public bool passed;
	
	
	CubeGenerator cg;
	

	// Use this for initialization
	void Start () {

		passed = false;
		cg = GameObject.Find("CubeGen").GetComponent<CubeGenerator>();
		width = (int)cg.Size.x;
	
	}

	void OnDisable() {
		if(cg && passed) cg.Extend ();
	}

	// Update is called once per frame
	void FixedUpdate () {

		if(PlayerObject.transform.position.z > transform.position.z + diff)
		{
			passed = true;
		}

		if (passed)
		{
			if(gameObject.GetComponent<Rigidbody>() == null){
				gameObject.AddComponent("Rigidbody");
			}
			gameObject.GetComponent<Rigidbody>().isKinematic = false;
			gameObject.GetComponent<BoxCollider>().isTrigger = false;

			gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0.2f,-1.3f,0.0f) * Random.value ,ForceMode.Impulse);

			
			Destroy(gameObject,Decay);
			
		}
	
	}
	
}
