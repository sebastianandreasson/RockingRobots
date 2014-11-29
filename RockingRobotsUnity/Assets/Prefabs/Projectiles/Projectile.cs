using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
		public float levelRequiredForProjectile;
		public Shader positiveGlowShader;
		private Shader defaultShader;
	
		public float timeToGlow;
		private float timeToEndGlow;
		
		private float timeToStartParticleEffects;
		
		public GameObject stoneSplosionEffect;
	
		// Use this for initialization
		void Start ()
		{
				defaultShader = renderer.material.shader;
				timeToStartParticleEffects = Time.time + 5.0f;
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
						if (!audio.isPlaying) {
								audio.Play ();
						}
						playStoneEffect ();
				}
		}
		
		void playStoneEffect ()
		{
				if (Time.time > timeToStartParticleEffects) {
						GameObject particleEffect = (GameObject)Instantiate (stoneSplosionEffect, gameObject.transform.position, Quaternion.identity);		
						Destroy (particleEffect, 5);
				}
		}
}
