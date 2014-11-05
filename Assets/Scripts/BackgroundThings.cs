using UnityEngine;
using System.Collections;

public class BackgroundThings : MonoBehaviour {
	public GameObject ring;
	public GameObject cone;
	public GameObject rib;
	public GameObject cg;
	public GameObject Player;
	GameObject ring1 ;
	GameObject ring2 ;
	GameObject ring3 ;
	float ring1Rot,ring2Rot,ring3Rot;
	Transform tf;
	// Use this for initialization
	void Start () {
		tf = GetComponent<Transform>();
		cg = GameObject.Find("CubeGen");
		Player = cg.GetComponent<CubeGenerator>().PlayerObject;
		ring1 = (GameObject) Instantiate(ring, new Vector3(cg.GetComponent<CubeGenerator>().Size.x/2, 
		                                                              0,
		                                                              Player.transform.position.z + 10f),
		                                            Quaternion.identity);
		ring1Rot = Random.Range(1,10);
		
		ring2 = (GameObject) Instantiate(ring, new Vector3(cg.GetComponent<CubeGenerator>().Size.x/2, 
		                                                   0,
		                                                   Player.transform.position.z + 15f),
		                                 Quaternion.identity);
		ring2Rot = Random.Range(1,10);
		ring3 = (GameObject) Instantiate(ring, new Vector3(cg.GetComponent<CubeGenerator>().Size.x/2, 
		                                                   0,
		                                                   Player.transform.position.z + 20f),
		                                 Quaternion.identity);
		ring3Rot = Random.Range(10,25);
		
	}
	
	// Update is called once per frame
	void Update () {
		//GameObject cone1 = Instantiate(cone, 
		ring1.transform.Rotate(0,0,ring1Rot *Master.ratio());
		ring2.transform.Rotate(0,0,ring2Rot *Master.ratio());
		ring3.transform.Rotate(0,0,ring3Rot *Master.ratio());
		if (ring1.transform.position.z < Camera.main.transform.position.z){
			Destroy(ring1);
			ring1 = (GameObject) Instantiate(ring, new Vector3(cg.GetComponent<CubeGenerator>().Size.x/2, 
			                                                 0,
			                                                 Player.transform.position.z+10f),
			                               Quaternion.identity);
		}
		if (ring2.transform.position.z < Camera.main.transform.position.z){
			Destroy(ring2);
			ring2 = (GameObject) Instantiate(ring, new Vector3(cg.GetComponent<CubeGenerator>().Size.x/2, 
			                                                   0,
			                                                   Player.transform.position.z+10f),
			                                 Quaternion.identity);
		}
		if (ring3.transform.position.z < Camera.main.transform.position.z){
			Destroy(ring3);
			ring3 = (GameObject) Instantiate(ring, new Vector3(cg.GetComponent<CubeGenerator>().Size.x/2, 
			                                                   0,
			                                                   Player.transform.position.z+10f),
			                                 Quaternion.identity);
		}
		                               
	
	}
}
