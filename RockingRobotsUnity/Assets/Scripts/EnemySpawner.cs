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
		public float timeBetweenMonstersInEachWave;
		float timeForNextMonsterSpawnInWave;
		int currentWave;
		ArrayList bigEnemiesInEachWave;
		ArrayList smallEnemiesInEachWave;
	
	
	
	
		// Use this for initialization
		void Start ()
		{
				timeForNextWave = Time.time;
				bigEnemiesInEachWave = new ArrayList ();
				bigEnemiesInEachWave.Add (0);
				bigEnemiesInEachWave.Add (1);
				bigEnemiesInEachWave.Add (2);
				bigEnemiesInEachWave.Add (3);
				bigEnemiesInEachWave.Add (4);
				bigEnemiesInEachWave.Add (5);
				bigEnemiesInEachWave.Add (6);
				bigEnemiesInEachWave.Add (7);
				bigEnemiesInEachWave.Add (8);
				bigEnemiesInEachWave.Add (9);
		
				smallEnemiesInEachWave = new ArrayList ();
				smallEnemiesInEachWave.Add (10);
				smallEnemiesInEachWave.Add (10);
				smallEnemiesInEachWave.Add (11);
				smallEnemiesInEachWave.Add (11);
				smallEnemiesInEachWave.Add (12);
				smallEnemiesInEachWave.Add (12);
				smallEnemiesInEachWave.Add (13);
				smallEnemiesInEachWave.Add (13);
				smallEnemiesInEachWave.Add (14);
				smallEnemiesInEachWave.Add (14);
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (Time.time > timeForNextWave && Time.time > timeForNextMonsterSpawnInWave && (int)smallEnemiesInEachWave [currentWave] != 0 && (int)bigEnemiesInEachWave [currentWave] != 0) {
//						Debug.Log ("Want to spawn monsters");
						spawnMonstersForWave ();
//						Debug.Log ("next Monster: " + timeForNextMonsterSpawnInWave);
				} else if ((int)smallEnemiesInEachWave [currentWave] == 0 && (int)bigEnemiesInEachWave [currentWave] == 0) {
						waveIsFinished ();
				}
		}
	
		void spawnMonstersForWave ()
		{
				spawnMonsterAtPosition (prefabForCurrentWave (), spawnPoint1.transform.position);
				spawnMonsterAtPosition (prefabForCurrentWave (), spawnPoint2.transform.position);
				spawnMonsterAtPosition (prefabForCurrentWave (), spawnPoint3.transform.position);
				timeForNextMonsterSpawnInWave = Time.time + timeBetweenMonstersInEachWave;
		}
		
		GameObject prefabForCurrentWave ()
		{
		
				float chanceForABigOne;
				
				if ((int)bigEnemiesInEachWave [currentWave] == 0) {
						chanceForABigOne = 0;
				} else if ((int)smallEnemiesInEachWave [currentWave] == 0) {
						chanceForABigOne = 1;
				} else {
						chanceForABigOne = (float)bigEnemiesInEachWave [currentWave] / (float)smallEnemiesInEachWave [currentWave];
				}
				bool shouldWePopBigOne = Random.Range (0, 1) <= chanceForABigOne;
				GameObject prefabToReturn = null;
				if (shouldWePopBigOne) {
						prefabToReturn = bigEnemy;
						bigEnemiesInEachWave [currentWave] = (int)bigEnemiesInEachWave [currentWave] - 1;
				} else {
						prefabToReturn = smallEnemy;
						smallEnemiesInEachWave [currentWave] = (int)bigEnemiesInEachWave [currentWave] - 1;
				}
				
				return prefabToReturn;
		}
	
		void waveIsFinished ()
		{
				Debug.Log ("Wave is finished");
				timeForNextWave = Time.time + timeBetweenWaves;
		}
	
		void spawnMonsterAtPosition (GameObject monster, Vector3 position)
		{
				if (monster != null) {
						Debug.Log ("spawning monster!!!!!!!!");
						Instantiate (smallEnemy, position, Quaternion.identity);
				}
		}
}
