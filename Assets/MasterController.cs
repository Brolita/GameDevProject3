using UnityEngine;
using System.Collections;

public class Master : MonoBehaviour {
	
	public static float value;
	public static float target;
	public static float speed;
	public static float effectLength;

	private static float dv;
	private static float d2v;
	private static float localValueMax;
	
	public  static float dt = .05f;

	private static float a = 1;
	private static float b = -3;
	private static float c = 2;

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
		Master.maxSpeed *= 1 + Mathf.Exp (2); 
	}

	public static bool update() {
		// effect length
		if(Master.effectLength > 0) {					// if were in an effect
			Master.effectLength -= Master.dt;			// decement the effect length
			if (Master.effectLength <= 0) {				// if we've passed the effect length
				Master.effectLength = 0;				// set the length to 0
				Master.target = -localValueMax;			// target with withdrawl amount
			}
		}

		// Second order control creates a force 
		float force = a*(Master.value - Master.target) + b*(Master.dv/Master.dt) + c*(Master.d2v/Master.dt);
		Master.d2v += force; 							// apply force
		Master.dv += Master.d2v;						// change velocity 
		Master.value += Master.dv;						// update value

		// check for new local max
		if (Master.value > Master.localValueMax) 
			Master.localValueMax = Master.value;

		//calculate the speed for this step
		Master.speed = Master.maxSpeed / (1f + Mathf.Exp (Master.value / 200));

		if (Master.value > Master.PONRMax) {
			Debug.Log("Point of no return Max past");
			return false;
		}

		if (Master.value < Master.PONRMin) {
			Debug.Log("Point of no return Min past");
			return false;
		}

		Debug.Log ("Value: " + Master.value + " Target: " + Master.target);

		return true;
	}
}
