using UnityEngine;
using System.Collections;

public class LevelingScript : MonoBehaviour
{

		public float experience;
		public float levelDivider = 5.0F;
	
		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
	
	
		public void addExperience (int experienceToAdd)
		{
				experience = + experienceToAdd;
		}
		
		public float level ()
		{
				return (experience / levelDivider);
		}
}
