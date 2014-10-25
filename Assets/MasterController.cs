using UnityEngine;
using System.Collections;

public class Master : MonoBehaviour {
	
	public static float value;
	public static float target;
	public static float speed;

	private static float _effectLength = 0;
	public static float effectLength {
		get {
			return _effectLength;
		}
		set {
			if (value < Master._effectLength) {		// this property causes growth asymptotically to sqrt(x) 
				Master._effectLength = value;       // but allows linear diminishing
			} else {								
				_effectLength = Mathf.Sqrt(Master._effectLength * Master._effectLength - Master._effectLength + value);
			}
		}
	}

	private static float dv;
	private static float d2v;
	private static float localValueMax;
	
	public  static float dt = .05f;

	private static float a = .005f;
	private static float b = -1;
	private static float c = 1;

	private static float maxSpeed = 1f;			
	private static float PONRMax = 400f;
	private static float realMax = 420f;
	private static float PONRMin = -360f;
	private static float realMin = -380f;
	
	public static void start() {
		Master.value = 0;
		Master.target = 0;
		Master.effectLength = 0;
		Master.dv = 0;
		Master.d2v = 0;
		Master.localValueMax = 0;
		// max speed is multiplied to allow it to actually reach its max value
		Master.maxSpeed *= 1 + Mathf.Exp (-2); 
	}

	public static bool update() {
		// effect length
		if(Master.effectLength > 0) {					// if were in an effect
			Master.effectLength -= Master.dt;			// decement the effect length
			if (Master.effectLength <= 0) {				// if we've passed the effect length
				Master._effectLength = 0;				// set the length to 0
				Master.target = -localValueMax;			// target with withdrawl amount
			}
		}

		// Second order control creates a force 
		Master.d2v = a*(Master.target - Master.value) + b*(Master.dv)*(Master.dt) + c*(Master.d2v)*(Master.dt)*(Master.dt);
		Master.dv += Master.d2v;						// change velocity 
		Master.value += Master.dv;						// update value

		// check for new local max
		if (Master.value > Master.target && Master.target > Master.localValueMax) 
			Master.localValueMax = Master.target;

		//calculate the speed for this step
		Master.speed = Master.maxSpeed / (1f + Mathf.Exp (- Master.value / 200));

		if (Master.target > Master.PONRMax) {
			Debug.Log("Point of no return Max past");
			return true;
		}

		if (Master.target < Master.PONRMin) {
			Debug.Log("Point of no return Min past");
			return true;
		}

		Debug.Log ("Speed: " + Master.speed + " Value: " + Master.value + " Target: " + Master.target + " Effect Length: " + Master.effectLength);

		return false;
	}
}
