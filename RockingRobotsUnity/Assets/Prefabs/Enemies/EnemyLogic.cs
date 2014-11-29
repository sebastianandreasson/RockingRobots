using UnityEngine;
using System.Collections;

public class EnemyLogic : MonoBehaviour
{

		public float health = 5;
		GameObject player;
		public GameObject stoneSplosionParticleEffect;

		// Use this for initialization
		void Start ()
		{
				player = GameObject.Find ("Player");
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
		
		
		void OnCollisionEnter (Collision collision)
		{
				Debug.Log ("we are in collision enter for monster!!");
				playStoneEffectIfGameObjectIsAboveWalkingSpeedAndLayerIsStoneLayer (gameObject, collision.gameObject.layer);
	
		}
		
		void playStoneEffectIfGameObjectIsAboveWalkingSpeedAndLayerIsStoneLayer (GameObject movingObject, int possibleLayer)
		{
				int stoneLayer = 8;
				bool layerIsStone = possibleLayer == stoneLayer;
				//check if velocity is high speed
				int highSpeedLimit = 5;
				bool velocityIsOverWalkingSpeed = movingObject.rigidbody.velocity.magnitude > highSpeedLimit;
				//if it is, we something is fast. Play particle effect!
				if (layerIsStone && velocityIsOverWalkingSpeed) {
						Debug.Log ("want to play stone particle effect");
						playStoneParticleEffect ();
				}
		
		}
		
		void playStoneParticleEffect ()
		{
				Instantiate (stoneSplosionParticleEffect, Vector3.zero, Quaternion.identity);
		}
		void OnCollisionExit (Collision collision)
		{
				Projectile thePossibleProjectileScript = collision.gameObject.GetComponent<Projectile> ();
				if (thePossibleProjectileScript != null) {
				
						playStoneEffectIfGameObjectIsAboveWalkingSpeedAndLayerIsStoneLayer (collision.gameObject, collision.gameObject.layer);
						//it was a projectile that hit
						//calculate damage based on resulting velocity
						Debug.Log ("Projectile has hit!");
						float resultingVelocity = gameObject.rigidbody.velocity.magnitude;	
						takeDamageBasedOnVelocity (resultingVelocity);			
				}				
				
		}
		
		void takeDamageBasedOnVelocity (float velocity)
		{
				health -= velocity;
				Debug.Log ("velocity after hit: " + velocity);
				Debug.Log ("health after hit: " + health);
				if (health <= 0) {
						Die ();
				}
		
		}
		
		void Die ()
		{
				rigidbody.freezeRotation = false;
				Debug.Log ("MONSTER DEAD. DO ANIMATION");
				LevelingScript playerLevelScript = player.GetComponent<LevelingScript> ();
				playerLevelScript.addExperience (1);
				Invoke ("RemoveObjectFromScene", 4);
		}
		
		void RemoveObjectFromScene ()
		{
				Destroy (gameObject);
		}
		
}
