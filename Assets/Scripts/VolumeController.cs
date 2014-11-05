using UnityEngine;
using System.Collections;

public class VolumeController : MonoBehaviour {
	// Update is called once per frame
	void Update () {
		GetComponent<AudioSource> ().volume = Master.ratio ();
	}
}
