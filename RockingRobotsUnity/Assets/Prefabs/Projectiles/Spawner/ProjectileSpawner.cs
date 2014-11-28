using UnityEngine;
using System.Collections;

public class ProjectileSpawner : MonoBehaviour
{
		public GameObject size1Prefab;
	
		// Use this for initialization
		void Start ()
		{
				Vector3 spawnVector = new Vector3 (0, 0, 0);
				for (int i = 0; i < 10; i ++) {
						spawnVector = spawnVector + new Vector3 (2, 0, 0);
						spawnSize1AtVector (spawnVector);
				}
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
		
		
		void spawnSize1AtVector (Vector3 spawnPoint)
		{
				Instantiate (size1Prefab, spawnPoint, Quaternion.identity);
		}
}
