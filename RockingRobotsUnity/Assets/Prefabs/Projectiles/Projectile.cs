using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
		public float levelRequiredForProjectile;
		public Shader positiveGlowShader;
		private Shader defaultShader;
	
		public float timeToGlow;
		private float timeToEndGlow;
		
		public GameObject stoneSplosionEffect;
	
		// Use this for initialization
		void Start ()
		{
				defaultShader = renderer.material.shader;
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (Time.time >= timeToEndGlow) {
						revertBackToRegularShader ();
				}
		}
	
	
		public bool playerIsAimingAtThisObjectWithLevelCanWePickUp (float playerLevel)
		{
				timeToEndGlow = Time.time + timeToGlow;
				bool canPlayerPickUp = playerLevel >= levelRequiredForProjectile;
				Debug.Log ("player can pick up: " + canPlayerPickUp);
				if (canPlayerPickUp) {
						showPositiveGlowShader ();
				}
				return canPlayerPickUp;
		}
	
	
		void showPositiveGlowShader ()
		{
				renderer.material.shader = positiveGlowShader;
		}

	
		void revertBackToRegularShader ()
		{
				renderer.material.shader = defaultShader;
		}
		
		void OnCollisionEnter (Collision collision)
		{
				if (gameObject.rigidbody.velocity.magnitude > 5) {
						playStoneEffect ();
				}
		}
		
		void playStoneEffect ()
		{
				Instantiate (stoneSplosionEffect, gameObject.transform.position, Quaternion.identity);		
		}
}
