using UnityEngine;
using System.Collections;

[RequireComponent (typeof (BoxCollider))]

public class RopeAttacher : MonoBehaviour {
	private BoxCollider coll;
	private RopeScript[] ropes = new RopeScript[12];
	public float resolution = 10f;
	private float _vibIntensity = 10f;
	public float vibIntensity {
		get {
			return _vibIntensity;
		}
		set {
			for(int i = 0; i < ropes.Length; i++) {
				ropes[i].GetComponent<RopeScript>().vibIntensity = value;
				_vibIntensity = value;
			}
		}
	}
	private int _vibRate = 10;
	public int vibRate {
		get {
			return _vibRate;
		}
		set {
			for(int i = 0; i < ropes.Length; i++) {
				ropes[i].GetComponent<RopeScript>().vibRate = value;
				_vibRate = value;
			}
		}
	}

	void Awake () {
		coll = GetComponent<BoxCollider> ();
		Vector3 top = coll.center + coll.size / 2;
		Vector3 bot = coll.center - coll.size / 2;
		ropes [0] = CreateRopeHead (top);
		ropes [0].target = CreateRopeTail (top - new Vector3 (coll.size.x, 0, 0));
		ropes [1] = CreateRopeHead (top);
		ropes [1].target = CreateRopeTail (top - new Vector3 (0, coll.size.y, 0));
		ropes [2] = CreateRopeHead (top);
		ropes [2].target = CreateRopeTail (top - new Vector3 (0, 0, coll.size.z));

		ropes [3] = CreateRopeHead (bot);
		ropes [3].target = CreateRopeTail (bot + new Vector3 (coll.size.x, 0, 0));
		ropes [4] = CreateRopeHead (bot);
		ropes [4].target = CreateRopeTail (bot + new Vector3 (0, coll.size.y, 0));
		ropes [5] = CreateRopeHead (bot);
		ropes [5].target = CreateRopeTail (bot + new Vector3 (0, 0, coll.size.z));

		ropes [6] = CreateRopeHead (top - new Vector3 (coll.size.x, coll.size.y, 0));
		ropes [6].target = CreateRopeTail (top - new Vector3 (coll.size.x, 0, 0));
		ropes [7] = CreateRopeHead (top - new Vector3 (coll.size.x, coll.size.y, 0));
		ropes [7].target = CreateRopeTail (top - new Vector3 (0, coll.size.y, 0));

		ropes [8] = CreateRopeHead (top - new Vector3 (coll.size.x, 0, coll.size.z));
		ropes [8].target = CreateRopeTail (top - new Vector3 (coll.size.x, 0, 0));
		ropes [9] = CreateRopeHead (top - new Vector3 (coll.size.x, 0, coll.size.z));
		ropes [9].target = CreateRopeTail (top - new Vector3 (0, 0, coll.size.z));

		ropes [10] = CreateRopeHead (top - new Vector3 (0, coll.size.y, coll.size.z));
		ropes [10].target = CreateRopeTail (top - new Vector3 (0, coll.size.y, 0));
		ropes [11] = CreateRopeHead (top - new Vector3 (0, coll.size.y, coll.size.z));
		ropes [11].target = CreateRopeTail (top - new Vector3 (0, 0, coll.size.z));

		for (int i = 0; i < 12; i++) {
			ropes[i].BuildRope();
			//ropes[i].vibIntensity = _vibIntensity;
			//ropes[i].vibRate = _vibRate;
		}
	}

	RopeScript CreateRopeHead (Vector3 pos) {
		GameObject newRopeHead = new GameObject ();
		Rigidbody rigid = newRopeHead.AddComponent<Rigidbody> ();
		LineRenderer line = newRopeHead.AddComponent<LineRenderer> ();
		line.SetWidth (.04f, .04f);
		RopeScript rope = newRopeHead.AddComponent<RopeScript> ();
		rigid.isKinematic = true;
		rope.resolution = resolution;
		newRopeHead.transform.parent = transform;
		newRopeHead.transform.position = pos + transform.position;
		return rope;
	}

	Transform CreateRopeTail (Vector3 pos) {
		GameObject newRopeTail = new GameObject ();
		Rigidbody rigid = newRopeTail.AddComponent<Rigidbody> ();
		rigid.isKinematic = true;
		newRopeTail.transform.parent = transform;
		newRopeTail.transform.position = pos + transform.position;
		return newRopeTail.transform;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
