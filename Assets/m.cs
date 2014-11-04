using UnityEngine;
using System.Collections;

public class m : MonoBehaviour {
	public Material skybox;
	public Material invertedSkybox;

	void Start () {
		Master.start (skybox, invertedSkybox);
		StartCoroutine (mUpdate ());
	}
	void Update() {
		if (Input.GetKeyDown ("w")) {
			Master.target += 10;
			Master.effectLength += 2;
		}
		GetComponent<Light> ().intensity = 2*Master.ratio();
	}
	IEnumerator mUpdate() {
		if (Master.update ()) {
			Debug.Log ("Done");
			yield break;
		}
		yield return new WaitForSeconds (Master.dt);
		StartCoroutine (mUpdate ());
	}
}
