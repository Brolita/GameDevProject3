using UnityEngine;
using System.Collections;

public class Remover : MonoBehaviour {
	int frame;
	void Start () 
	{
		frame = 0;
	}
	void Update () 
	{
		frame ++;
		if (frame == 6) Destroy (gameObject);
	}
}
