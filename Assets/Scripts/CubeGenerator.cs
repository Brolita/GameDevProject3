	using UnityEngine;
	using System.Collections;

	public class CubeGenerator : MonoBehaviour 
	{		
		public GameObject PlayerObject;
		public GameObject Platform;
		public GameObject Spike;
		public GameObject Drug;
		public Vector3 Size;
		public float OffsetFactor;
		/// <summary>
		/// The drug spawn rate is how many blocks between the average drug spawn.
		/// </summary>
		public float drugSpawnRate;
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

		private int[] depth;
		private bool[] path;
		private int lastDrugSpawn;

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

		GameObject newPlatform;
		void Update ()
		{
			GetComponent<BoxCollider>().center = new Vector3 (Size.x / 2f -.5f, -.5f, PlayerObject.transform.position.z + ((end - PlayerObject.transform.position.z) / 2f));
			GetComponent<BoxCollider> ().size = new Vector3 (Size.x, Size.y, end - PlayerObject.transform.position.z);	
		}
		
		void Start () 
		{
			Platform.GetComponent<CubeScript>().PlayerObject = PlayerObject;
			speed = Platform.GetComponent<CubeScript>().Speed;

			// ----- Base Case ------

			depth = new int[(int)Size.x];
			path = new bool[(int)Size.x];
			for(int z = 0; z < 20; z++) {
				for(int x = 0 ; x < Size.x ; x++)
				{
					path[x] = true;
					depth[x] = 1;
					newPlatform = (GameObject)Instantiate(Platform,  (transform.position + new Vector3(x, 0, z)) * (1 + OffsetFactor), transform.rotation);
					newPlatform.transform.parent = transform;
					newPlatform.GetComponent<CubeScript>().PlayerObject = PlayerObject;
				}
				end = z + 1;
			}

			// -----------------------

			for(int z = 20 ; z < Size.z ; z++)
			{
				Generate(z);
				end += 1;
			}

			lastDrugSpawn = (int)end;
		}

		void Create(float x, float z, bool spike = false, bool makeGreen = false) 
		{
			if(!spike) newPlatform = (GameObject)Instantiate(Platform,  (transform.position + new Vector3(x, 0, z)) * (1 + OffsetFactor), transform.rotation);
			else       newPlatform = (GameObject)Instantiate(Spike,  (transform.position + new Vector3(x, 0, z)) * (1 + OffsetFactor), transform.rotation);
			newPlatform.transform.Rotate(90 * Random.Range(0,4) * new Vector3(0, 1, 0));
			newPlatform.transform.parent = transform;
			newPlatform.GetComponent<CubeScript>().PlayerObject = PlayerObject;
		}

		void CreateDrug(float x, float z)
		{
			GameObject drug = (GameObject)Instantiate (Drug, (transform.position + new Vector3 (x, 1, z)) * (1 + OffsetFactor), transform.rotation);
			drug.transform.parent = transform;
			drug.GetComponent<PowerUpScript>().PlayerObject = PlayerObject;
		}

		int Generate(float z, bool firstPass = true) 
		{
			
			if(Master.done)
			{
				for(int x = 0 ; x < Size.x ; x++)
				{
					Create(x, z);
				}
				return 0;
			}
			
			drugSpawnRate = 25 - Mathf.Floor (Master.localValueMax / 20f) - Mathf.Floor (10 - Master.ratio () * 10);
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

			// ---------------------------------
			// ------- pass, create nodes ------

			for(int x = 0 ; x < Size.x ; x++)
			{
				if(depth[x] != 0) Create(x, z, false, path[x]);
				else Create(x, z, true, false);
			}

			// ---------------------------------
			// ---- check to generate drugs ----

			if (z - lastDrugSpawn > drugSpawnRate) {
				int count = 0;
				for(int x = 0 ; x < Size.x ; x++)
				{
					count += path[x] ? 1 : 0;
				}
				count -= (int)Mathf.Floor(Random.value * count);
				for(int x = 0 ; x < Size.x ; x++)
				{
					count -= path[x] ? 1 : 0;
					if(count == 0 && path[x]) CreateDrug(x,z);
				}
				lastDrugSpawn = (int) z;
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
			if ( n == Size.x ) 
			{
				Generate(end);
				n = 0;
				end += 1;
			}
		}
	}