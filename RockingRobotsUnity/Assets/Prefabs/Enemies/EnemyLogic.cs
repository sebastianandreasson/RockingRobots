using UnityEngine;
using System.Collections;

public class EnemyLogic : MonoBehaviour
{

		public float health;
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
				Debug.Log ("we are in collision enter!!");
				//check if layer is stone
				int stoneLayer = 8;
				bool layerIsStone = collision.gameObject.layer == stoneLayer;
				//check if velocity is high speed
				int highSpeedLimit = 10;
				bool velocityIsOverWalkingSpeed = gameObject.rigidbody.velocity.magnitude > highSpeedLimit;
				//if it is, we are flying into stone. Play particle effect!
				
				Debug.Log ("we are in collision enter!! Stone:" + layerIsStone + " velocity: " + velocityIsOverWalkingSpeed);
				if (layerIsStone && velocityIsOverWalkingSpeed) {
						playStoneParticleEffect ();
				}
	
		}
		
		void playStoneParticleEffect ()
		{
				Instantiate (stoneSplosionParticleEffect, Vector3.zero, Quaternion.identity);
		}
		void OnCollisionExit (Collision collision)
		{
				Debug.Log ("Something has hit!");
		
				Projectile thePossibleProjectileScript = collision.gameObject.GetComponent<Projectile> ();
				if (thePossibleProjectileScript != null) {
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
