using UnityEngine;
using System.Collections;

public class RemoveSpikes : MonoBehaviour 
{
	void OnTriggerEnter(Collider other) 
	{
		print(transform.position.y);
		if(other.tag == "Remover")
		{
			Transform[] children = GetComponentsInChildren<Transform>();
			for(int i = 0; i < children.Length; i++) 
			{
				if(children[i].tag == "Spike")
					Destroy(children[i].gameObject);
			}
		}
	}
}
