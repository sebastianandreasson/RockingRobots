using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
	
		public GameObject spawnPoint1;
		public GameObject spawnPoint2;
		public GameObject spawnPoint3;
	
		public GameObject smallEnemy;
		public GameObject bigEnemy;
	
		public float timeBetweenWaves;
		float timeForNextWave;
		public float monstersInEachWave;
		public float timeBetweenMonstersInEachWave;
		float timeForNextMonsterSpawnInWave;
		int monstersSpawnedThisWave;
	
	
	
		// Use this for initialization
		void Start ()
		{
				timeForNextWave = Time.time;
				monstersSpawnedThisWave = 0;
		
		}
	
		// Update is called once per frame
		void Update ()
		{
				
				
				if (Time.time > timeForNextWave && monstersSpawnedThisWave < monstersInEachWave && Time.time > timeForNextMonsterSpawnInWave) {
						Debug.Log ("Want to spawn monsters");
						spawnMonstersForWave ();
						Debug.Log ("next Monster: " + timeForNextMonsterSpawnInWave);
				} else if (Time.time > timeForNextWave && monstersSpawnedThisWave >= monstersInEachWave) {
						waveIsFinished ();
				}
		}
	
		void spawnMonstersForWave ()
		{
				spawnMonsterAtPosition (spawnPoint1.transform.position);
				spawnMonsterAtPosition (spawnPoint2.transform.position);
				spawnMonsterAtPosition (spawnPoint3.transform.position);
				monstersSpawnedThisWave += 3;
				timeForNextMonsterSpawnInWave = Time.time + timeBetweenMonstersInEachWave;
		}
	
		void waveIsFinished ()
		{
				Debug.Log ("Wave is finished");
				monstersSpawnedThisWave = 0;
				timeForNextWave = Time.time + timeBetweenWaves;
		}
	
		void spawnMonsterAtPosition (Vector3 position)
		{
				Debug.Log ("spawning monster!!!!!!!!");
				Instantiate (smallEnemy, position, Quaternion.identity);
		
		}
}
