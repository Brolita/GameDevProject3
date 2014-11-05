using UnityEngine;
using System.Collections;

public class BackgroundThings : MonoBehaviour {
	public GameObject ring;
	public GameObject cone;
	public GameObject rib;
	GameObject cg;
	GameObject Player;

	// Use this for initialization
	void Start () {
		cg = GameObject.Find("CubeGen");
		Player = cg.GetComponent<CubeGenerator>().PlayerObject;

	}
	
	// Update is called once per frame
	void Update () {
		//GameObject cone1 = Instantiate(cone,
	
	}
}
