using UnityEngine;
using System.Collections;

public class Master {
	
	public static float value;
	public static float target;
	public static float speed;
	public static bool done = false;

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

	private static Camera camera;
	private static Material skybox;
	private static Material skyboxInverted;
	private static int freeze = 0;

	private static float dv;
	private static float d2v;
	public static float localValueMax;
	
	public  static float dt = .05f;

	private static float a = .005f;
	private static float b = -2;
	private static float c = 1;

	private static float maxSpeed = 1f;			
	public static float PONRMax = 400f;
	public static float realMax = 420f;
	public static float PONRMin = -360f;
	public static float realMin = -380f;
	
	public static void start(Material s, Material sI) {
		Master.value = 0;
		Master.target = 0;
		Master.effectLength = 0;
		Master.dv = 0;
		Master.d2v = 0;
		Master.localValueMax = 0;
		Master.skybox = s;
		Master.skyboxInverted = sI;
		// max speed is multiplied to allow it to actually reach its max value
		Master.maxSpeed *= 1 + Mathf.Exp (-2); 
	}
	
	public static bool update() {

		if (Master.target > Master.PONRMax && Master.value > Master.PONRMax) {
			Master.done = true;
			return true;
		}
		
		if (Master.value <= Master.PONRMin && Master.localValueMax >= -Master.PONRMin) {
			Master.done = true;
			return true;
		}

		if (freeze != 0) {
			freeze --;
			if(freeze == 0) {
				RenderSettings.skybox = Master.skybox;
				Master.target = -Master.localValueMax;
			}
			return false;
		}
		// effect length
		if(Master.effectLength > 0) {					// if were in an effect
			Master.effectLength -= Master.dt;			// decement the effect length
			if (Master.effectLength <= 0) {				// if we've passed the effect length
				Master._effectLength = 0;				// set the length to 0
				Master.target = -Master.localValueMax;	// target with withdrawl amount
			}
		}

		// Second order control creates a force 
		Master.d2v = a*(Master.target - Master.value) + b*(Master.dv)*(Master.dt) + c*(Master.d2v)*(Master.dt)*(Master.dt);
		Master.dv += Master.d2v;						// change velocity 
		Master.value += Master.dv;						// update value

		// check for new local max
		if (Master.value >= Master.target && Master.target > Master.localValueMax) 
			Master.localValueMax = Master.target;

		//calculate the speed for this step
		Master.speed = Master.maxSpeed / (1f + Mathf.Exp (- Master.value / 200));

		Debug.Log (Master.value);


		return false;
	}

	public static float ratio() {
		return (Master.value - Master.realMin) / (Master.realMax - Master.realMin);
	}

	public static void hit() {
		Master.freeze = 3;
		RenderSettings.skybox = Master.skyboxInverted;
		Master.value = Master.realMin;
		Master.target = Master.value;
		Master.speed = 0;
	}
}
