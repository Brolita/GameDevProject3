using UnityEngine;
using System.Collections;

public class CubeGenerator : MonoBehaviour 
{		
	public GameObject PlayerObject;
	public GameObject Cube;
	public Vector3 Size;
	public float OffsetFactor;

	/// <summary>
	/// The spawn miss modifer is the amount of blocks
	/// on a straight path until there is a 50% chance it
	/// will not be generated
	/// </summary>
	public float spawnMissModifer; 
	/// <summary>
	/// The path miss buffer is the amount of time after a gap
	/// until a path is considered black-able 
	/// </summary>
	public int pathMissBuffer;
	/// <summary>
	/// The minimum path count is the minimum number of black
	/// nodes in any cross section until the path is thrown out
	/// </summary>
	public int minimumPathCount;

	public int[] depth;
	public bool[] path;

	/* Pseduo code / explaination of algorithm
	 * 
	 *  "black" nodes mean the player could be on them
	 * 	"red" nodes are considered not to exist because the
	 * 	player cannot be on them
	 * 
	 * 	Base case:
	 * 		All nodes are black
	 * 		All depths are 1
	 *  
	 * 	Generate:
	 * 		For each path:
	 * 			Spawn chance as a function of depth and (const) spawn miss modifier
	 * 			If we're spawning it:
	 * 				Increment the depth
	 * 				Increment the count
	 * 			Otherwise:
	 * 				Set the depth to 0
	 * 				Set the node to red
	 * 		
	 * 		If the count is less then minimum path count
	 * 			Undo changes to depth
	 * 			Return recurse to try again
	 *
	 * 		Loop through the nodes again:
	 * 			if depth is less than path miss buffer or the path is set behind it:
	 * 				it remains the same as the previous
	 *
	 *		Loop through the nodes again:
	 *			if the node is black and the adjectent node is not set:
	 *				the node is black
	 *				increment the count
	 * 
	 * 		If the count is less then the minimum path count
	 * 			Undo changes to depth and path
	 * 			Return recurse to try again
	 * 
	 * 		Loop through the nodes again:
	 * 			if the depth is not 0:
	 * 				create the node
	 * 				increment the count
	 * 	
	 * 		Return count to subtract from available resources
	 */ 

	float end = 0;
	float speed;
	float n = 0;

	GameObject newCube;

	void Start () 
	{
		Cube.GetComponent<CubeScript>().PlayerObject = PlayerObject;
		speed = Cube.GetComponent<CubeScript>().Speed;

		// ----- Base Case ------

		depth = new int[(int)Size.x];
		path = new bool[(int)Size.x];
		for(int x = 0 ; x < Size.x ; x++)
		{
			path[x] = true;
			depth[x] = 1;
			newCube = (GameObject)Instantiate(Cube,  (transform.position + new Vector3(x, 0, 0)) * (1 + OffsetFactor), transform.rotation);
			newCube.transform.parent = transform;
			end = 1;
		}

		// -----------------------

		for(int z = 1 ; z < Size.z ; z++)
		{
			Generate(z);
			end += 1;
		}
	}

	void Create(float x, float z, bool makeRed = false, bool makeGreen = false) 
	{
		newCube = (GameObject)Instantiate(Cube,  (transform.position + new Vector3(x, 0, z)) * (1 + OffsetFactor), transform.rotation);
		newCube.transform.parent = transform;
		if(makeRed) newCube.renderer.material.color = new Color (1, 0, 0);
		if(makeGreen) newCube.renderer.material.color = new Color (0, 1, 0);
	}

	int Generate(float z, bool firstPass = true) 
	{
		int i;
		int[] copyDepth = new int[(int)Size.x];
		bool[] copyPath = new bool[(int)Size.x];
		bool[] set = new bool[(int)Size.x];
		System.Array.Copy (depth, copyDepth, (int)Size.x);
		System.Array.Copy (path, copyPath, (int)Size.x);
		for (i = 0; i < Size.x; i++) 
		{
			set[i] = false;
		}
		// ---------------------------------
		// - generate random cross-section -

		i = 0;
		for(int x = 0 ; x < Size.x ; x++)
		{
			if (Random.value < spawnChance (x)) 
			{
				depth[x]++;
				i++;
			} 
			else 
			{
				depth[x] = 0;
				path[x] = false;
			}
		}

		// ---------------------------------
		// ---- first check enough nodes ---

		if (i < minimumPathCount) 
		{
			depth = copyDepth;
			path = copyPath;
			return Generate(z, false);
		}

		// ---------------------------------
		// ---- first pass for algorithm ---
	
		for(int x = 0 ; x < Size.x ; x++)
		{
			if (depth[x] <= pathMissBuffer || path[x]) 
				set[x] = true;
		}

		// ---------------------------------
		// --- second pass for algorithm ---

		for(int x = 0 ; x < Size.x ; x++)
		{
			if(set[x] && path[x]) 
			{
				if(x!=0 && !set[x-1])
					path[x-1] = true;
				if(x!=(set.Length - 1) && !set[x+1])
					path[x+1] = true;
			}
		}

		// ---------------------------------
		// --- second check enough nodes ---
		i = 0;
		for(int x = 0 ; x < Size.x ; x++)
		{
			if(path[x]) i++;
		}

		if (i < minimumPathCount) 
		{
			depth = copyDepth;
			path = copyPath;
			return Generate(z, false);
		}

		string st = "set: ";
		for (int j = 0; j < Size.x; j++) {
			st += set[j]?"true ":"false ";
		}
		st += " \t\tdepth: ";
		for (int j = 0; j < Size.x; j++) {
			st += "" + depth[j] + ' ';
		}
		st += "\t\tpath: ";
		for (int j = 0; j < Size.x; j++) {
			st += path[j]?"true ":"false ";
		}
		st += "\t count: " + i;
		//Debug.Log (st); 

		// ---------------------------------
		// ------- pass, create nodes ------

		for(int x = 0 ; x < Size.x ; x++)
		{
			if(depth[x] != 0) Create(x, z, false, path[x]);
			else Create(x, z, true, false);
		}
		return 0;
	}
	
	float spawnChance(int x) 
	{
		return 1 / (1 + Mathf.Exp (depth[x] - spawnMissModifer));
	}

	public void Extend() 
	{
		n++;
		Debug.Log (n);
		if ( n == Size.x ) 
		{
			Generate(end);
			n = 0;
			end += 1;
		}
	}
}