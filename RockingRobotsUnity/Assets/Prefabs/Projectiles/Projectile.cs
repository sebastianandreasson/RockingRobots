using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
		public Shader glowShader;
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
	
	
		public void playerIsAimingAtThisObject ()
		{
				timeToEndGlow = Time.time + timeToGlow;
				showGlowShader ();
		}
	
	
		void showGlowShader ()
		{
				renderer.material.shader = glowShader;
		}
	
		void revertBackToRegularShader ()
		{
				renderer.material.shader = defaultShader;
		}
}
