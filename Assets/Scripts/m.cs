using UnityEngine;
using System.Collections;

public class m : MonoBehaviour {
	public Material skybox;
	public Material invertedSkybox;
	public GameObject player;

	void Start () {
		Master.start (skybox, invertedSkybox);
		StartCoroutine (mUpdate ());
	}
	void Update() {
		GetComponent<Light> ().intensity = 2f*Master.ratio();


	}
	IEnumerator mUpdate() {
		if(Master.update ())
		{
			StartCoroutine (mCheckForEnd ());
			yield break;
		}
		if (player.transform.position.y < -8f) {
			StartCoroutine (mEnd2 ());
			yield break;
		}
		yield return new WaitForSeconds (Master.dt);
		StartCoroutine (mUpdate ());
	}

	IEnumerator mCheckForEnd() {
		if (Master.value <= Master.PONRMin) {
			StartCoroutine (mEnd1 ());
			yield break;
		} else if (player.transform.position.y < -8f) {
			StartCoroutine (mEnd2 ());
			yield break;
		} else if (player.rigidbody.velocity.z > 10f) {
			StartCoroutine (mEnd3 ());
			yield break;
		}
		yield return new WaitForSeconds (Master.dt);
		StartCoroutine (mCheckForEnd ());

	}

	IEnumerator mEnd1() {
		GameObject.Find ("Player").GetComponentInChildren<Camera> ().GetComponent<VolumeController> ().enabled = false;
		GameObject.Find ("Player").GetComponentInChildren<Camera> ().GetComponent<AudioSource> ().volume = .2f;
		yield return new WaitForSeconds (2);
		Transform[] platforms = GameObject.Find ("CubeGen").GetComponentsInChildren<Transform> ();
		for (int i = 0; i < platforms.Length; i++) {
			Destroy(platforms[i].gameObject);
		}
		GameObject.Find ("Player").GetComponentInChildren<Camera> ().GetComponent<AudioSource> ().volume = 1f;
		yield return new WaitForSeconds (3);
		Application.Quit ();
		yield break;
	}

	IEnumerator mEnd2() {
		Application.Quit ();
		yield break;
	}

	IEnumerator mEnd3() {
		if (player.rigidbody.velocity.z > 100f) {
			Application.Quit ();
			yield break;
		}
		yield return new WaitForSeconds (2);
	}
}
