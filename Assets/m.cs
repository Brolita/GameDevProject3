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
	}
	IEnumerator mUpdate() {
		Debug.Log ("Hello");
		if(Master.update ())
			yield return new WaitForSeconds (Master.dt);
	}
}
