﻿using UnityEngine;



public class FillView : MonoBehaviour
{

	void Update()
	{
		Camera cam = Camera.main;
		
		float pos = (cam.nearClipPlane + 0.01f);
		
		transform.position = cam.transform.position + cam.transform.forward * pos;
		
		float h = Mathf.Tan(cam.fov*Mathf.Deg2Rad*0.5f)*pos*2f;
		
		transform.localScale = new Vector3(h*cam.aspect,h,0f);

		transform.rotation = cam.transform.rotation;
	}
}