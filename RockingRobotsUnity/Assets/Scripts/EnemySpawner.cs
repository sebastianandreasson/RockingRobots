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
		ArrayList listOfPrefabOrders;
	
	
	
	
	
		// Use this for initialization
		void Start ()
		{
		
//				spawnMonsterAtPosition (bigEnemy, spawnPoint1.transform.position);
//				spawnMonsterAtPosition (bigEnemy, spawnPoint2.transform.position);
//				spawnMonsterAtPosition (bigEnemy, spawnPoint3.transform.position);

			
				timeForNextWave = Time.time;
				ArrayList bigEnemiesInEachWave = new ArrayList ();
				bigEnemiesInEachWave.Add (1);
				bigEnemiesInEachWave.Add (1);
				bigEnemiesInEachWave.Add (2);
				bigEnemiesInEachWave.Add (3);
				bigEnemiesInEachWave.Add (4);
				bigEnemiesInEachWave.Add (5);
				bigEnemiesInEachWave.Add (6);
				bigEnemiesInEachWave.Add (7);
				bigEnemiesInEachWave.Add (8);
				bigEnemiesInEachWave.Add (9);
		
				ArrayList smallEnemiesInEachWave = new ArrayList ();
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
				listOfPrefabOrders = new ArrayList (); 
				for (int i = 0; i < smallEnemiesInEachWave.Count; i++) {
						ArrayList prefabOrderForWave = new ArrayList ();
						//start off with three small enemies, then randomise
						prefabOrderForWave.Add (smallEnemy);
						prefabOrderForWave.Add (smallEnemy);
						prefabOrderForWave.Add (smallEnemy);
						smallEnemiesInEachWave [i] = (int)smallEnemiesInEachWave [i] - 3;
						while ((int)bigEnemiesInEachWave[i] > 0 || (int)smallEnemiesInEachWave[i] > 0) {
						
								float chanceOfBigOne = 0.2F;
								float newRandom = Random.Range (0, 1);
								bool shouldWePopBigOne = newRandom < chanceOfBigOne && (int)bigEnemiesInEachWave [i] > 0;
								
								if (shouldWePopBigOne) {
										prefabOrderForWave.Add (bigEnemy);
										bigEnemiesInEachWave [i] = (int)bigEnemiesInEachWave [i] - 1;
								} else {
										prefabOrderForWave.Add (smallEnemy);
										smallEnemiesInEachWave [i] = (int)smallEnemiesInEachWave [i] - 1;
								}
						}
						listOfPrefabOrders.Add (prefabOrderForWave);		
				}
				
				currentWave = 0;
		}
	
		// Update is called once per frame
		void Update ()
		{
				bool atLeastOneMonsterRemains = ((ArrayList)listOfPrefabOrders [currentWave]).Count > 0;
				if (Time.time > timeForNextWave && Time.time > timeForNextMonsterSpawnInWave && atLeastOneMonsterRemains) {
//						Debug.Log ("Want to spawn monsters");
						spawnMonstersForWave ();
//						Debug.Log ("next Monster: " + timeForNextMonsterSpawnInWave);
				} else if (!atLeastOneMonsterRemains) {
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
				GameObject prefabToReturn = null;
				if (((ArrayList)listOfPrefabOrders [currentWave]).Count > 0) {
						prefabToReturn = (GameObject)((ArrayList)listOfPrefabOrders [currentWave]) [0];
						((ArrayList)listOfPrefabOrders [currentWave]).RemoveAt (0);
				}

				return prefabToReturn;
		}
	
		void waveIsFinished ()
		{
				Debug.Log ("Wave is finished");
				currentWave++;
				Debug.Log ("Next wave has: " + ((ArrayList)listOfPrefabOrders [currentWave]).Count + " enemies");
			
				timeForNextWave = Time.time + timeBetweenWaves;
		}
	
		void spawnMonsterAtPosition (GameObject monster, Vector3 position)
		{
				
				if (monster != null) {
//						Debug.Log ("spawning monster!!!!!!!!");
						Instantiate (monster, position, Quaternion.identity);
				} else {
						Debug.Log ("wanted to spawn monster but it is nil");
				}
		}

}
