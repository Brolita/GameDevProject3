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

	void OnGUI()
	{
		if (Event.current.type == EventType.Repaint)
			Graphics.DrawTexture(new Rect(0,0,Screen.width, Screen.height), tex);
	}

	void OnPostRender()
	{

		if(!PostProcess) {
			//PostProcess = new Material();
		}
		GL.PushMatrix ();
		GL.LoadOrtho ();
		for (var i = 0; i <PostProcess.passCount; ++i) {
			PostProcess.SetPass (i);
			GL.Begin( GL.QUADS );
			GL.Vertex3( 0, 0, 0.1f );
			GL.Vertex3( 1, 0, 0.1f );
			GL.Vertex3( 1, 1, 0.1f );
			GL.Vertex3( 0, 1, 0.1f );
			GL.End();
		}
		GL.PopMatrix ();

		DestroyImmediate(tex);
		
		tex = new Texture2D(Mathf.FloorToInt(camera.pixelWidth), Mathf.FloorToInt(camera.pixelHeight));
		tex.filterMode = filterMode;
		tex.ReadPixels(new Rect(0, 0, camera.pixelWidth, camera.pixelHeight), 0, 0);
		tex.Apply();
	}

}
