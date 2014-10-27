using UnityEngine;
using System.Collections;

public class CubeGenerator : MonoBehaviour {
	
	
	public GameObject PlayerObject;
		
	public GameObject Cube;
	
	public Vector3 Size;
	
	public float OffsetFactor;
	
	float end = 0;
	
	float speed;
	
	float n = 0;
	
	GameObject newCube;
	
	void Start () {
		
	
	
		
		Cube.GetComponent<CubeScript>().PlayerObject = PlayerObject;
		
		speed = Cube.GetComponent<CubeScript>().Speed;
		

		
		for(int x = 0 ; x < Size.x ; x++)
		{
			for(int y = 0 ; y < Size.y ; y++)
			{
				for(int z = 0 ; z < Size.z ; z++)
				{
					if(Random.value > .8f){
						continue;
					}
					newCube = (GameObject)Instantiate(Cube,  (transform.position + new Vector3(x, y, z)) * (1 + OffsetFactor), transform.rotation);
					
					newCube.transform.parent = transform;
					
					end = z + 1;
			
				}
			
			}
			
		}
		
	
	}
	void Update(){

	}
	
	
	public void Extend() {
	
		
		n++;
			
		if ( n == Size.x ) {
			
			
			for(int x = 0 ; x < Size.x ; x++)
			{
				for(int y = 0 ; y < Size.y ; y++)
				{
					if(Random.value > .8f){
						continue;
					}
					newCube = (GameObject)Instantiate(Cube,  (transform.position + new Vector3(x, y, end)) * (1 + OffsetFactor), transform.rotation);
				
					newCube.transform.parent = transform;
					
				}
				
			
			}
			
			end += 1;
			n = 0;
		}
	}

	


}
