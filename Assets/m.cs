using UnityEngine;
using System.Collections;

public class m : MonoBehaviour {
	void Start () {
		Master.start ();
		StartCoroutine (mUpdate ());
	}
	void Update() {
		if (Input.GetKeyDown ("a")) {
			Master.target += 10;
			Master.effectLength += 2;
		}
		GetComponent<Light> ().intensity = 2 * (Master.value + 360) / (800);
	}
	IEnumerator mUpdate() {
		Debug.Log (Master.dt);
		if (Master.update ()) {
			Debug.Log ("Done");
			return true;
		}
		yield return new WaitForSeconds (Master.dt);
		StartCoroutine (mUpdate ());
	}
}
