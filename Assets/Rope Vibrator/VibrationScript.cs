using UnityEngine;
using System.Collections;

public class VibrationScript : MonoBehaviour {

	public float intensity;
	public int rate;
	int frame;
	
	// Use this for initialization
	void Start () {
		frame = (int) (rate * Random.value);
	}
	
	// Update is called once per frame
	void Update () {
		if(frame == rate) {
			rigidbody.AddForce(new Vector3( intensity*(Random.value - .5f), intensity * (Random.value - .5f), intensity * (Random.value - .5f)) );
			frame = (int) (rate * Random.value);
		}
		frame++;
	}
}
