using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {
	public AudioClip hitSound;
	public AudioClip drugPickup;

	public void spike () {
		GetComponent<AudioSource>().PlayOneShot(hitSound);
	}
	
	public void drug () {
		GetComponent<AudioSource>().PlayOneShot(drugPickup);
	}
}
