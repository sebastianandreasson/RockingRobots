using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
		public float levelRequiredForProjectile;
		public Shader positiveGlowShader;
		public Shader negativeGlowShader;
		private Shader defaultShader;
	
		public float timeToGlow;
		private float timeToEndGlow;
	
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
				if (canPlayerPickUp) {
						showPositiveGlowShader ();
				} else {
						showNegativeGlowShader ();
				}
				return canPlayerPickUp;
		}
	
	
		void showPositiveGlowShader ()
		{
				renderer.material.shader = positiveGlowShader;
		}
		void showNegativeGlowShader ()
		{
				renderer.material.shader = negativeGlowShader;
		}
	
		void revertBackToRegularShader ()
		{
				renderer.material.shader = defaultShader;
		}
}
