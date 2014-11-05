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
	// Use this for initialization
	void Start () {
		cg = GameObject.Find("CubeGen");
		Player = cg.GetComponent<CubeGenerator>().PlayerObject;
		ring1 = (GameObject) Instantiate(ring, new Vector3(cg.GetComponent<CubeGenerator>().Size.x/2, 
		                                                              0,
		                                                              Player.transform.position.z + 10f),
		                                            Quaternion.identity);
		ring2 = (GameObject) Instantiate(ring, new Vector3(cg.GetComponent<CubeGenerator>().Size.x/2, 
		                                                   0,
		                                                   Player.transform.position.z + 15f),
		                                 Quaternion.identity);
		ring3 = (GameObject) Instantiate(ring, new Vector3(cg.GetComponent<CubeGenerator>().Size.x/2, 
		                                                   0,
		                                                   Player.transform.position.z + 20f),
		                                 Quaternion.identity);
		
	}
	
	// Update is called once per frame
	void Update () {
		//GameObject cone1 = Instantiate(cone, 


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
