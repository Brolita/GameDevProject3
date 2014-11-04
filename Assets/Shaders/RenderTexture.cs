using UnityEngine;
using System.Collections;

public class RenderTexture : MonoBehaviour {



	Texture2D tex;
	public FilterMode filterMode = FilterMode.Point;
	public int pixelSize = 1;
	public Material PostProcess;

	// Use this for initialization
	void Start () {


	
	}
	
	// Update is called once per frame
	void Update () {
		camera.pixelRect = new Rect(0,0,Screen.width/pixelSize,Screen.height/pixelSize);
	
	}
	

	void OnPostRender()
	{



		DestroyImmediate(tex);
		
		tex = new Texture2D(Mathf.FloorToInt(camera.pixelWidth), Mathf.FloorToInt(camera.pixelHeight));
		tex.filterMode = filterMode;
		tex.ReadPixels(new Rect(0, 0, camera.pixelWidth, camera.pixelHeight), 0, 0);
		tex.Apply ();
		PostProcess.mainTexture = tex;

	
		



	}

}
