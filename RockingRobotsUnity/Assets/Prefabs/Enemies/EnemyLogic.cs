using UnityEngine;
using System.Collections;

public class EnemyLogic : MonoBehaviour
{

		public float health = 5;
		GameObject player;
		public GameObject fireEffect;

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
				if (gameObject.rigidbody.velocity.magnitude > 5) {
						Instantiate (fireEffect, gameObject.transform.position, Quaternion.identity);
				}
	
		}
		
		void OnCollisionExit (Collision collision)
		{
				Projectile thePossibleProjectileScript = collision.gameObject.GetComponent<Projectile> ();
				if (thePossibleProjectileScript != null) {
						//it was a projectile that hit
						//calculate damage based on resulting velocity
						Debug.Log ("Projectile has hit!");
						float resultingVelocity = collision.gameObject.rigidbody.velocity.magnitude;
						if (resultingVelocity > 5) {
								takeDamageBasedOnVelocity (resultingVelocity);			
						}
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
