using UnityEngine;
using System.Collections;

public class m : MonoBehaviour {
	void Start () {
		Master.start ();
		StartCoroutine (mUpdate ());
	}
	void Update() {
		if (Input.GetKeyDown ("w")) {
			Master.target += 10;
			Master.effectLength += 2;
		}
		GetComponent<Light> ().intensity = Master.ratio();
	}
	IEnumerator mUpdate() {
		if (Master.update ()) {
			Debug.Log ("Done");
			return true;
		}
		yield return new WaitForSeconds (Master.dt);
		StartCoroutine (mUpdate ());
	}
}
