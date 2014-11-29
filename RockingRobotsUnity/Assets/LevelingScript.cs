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
				float currentLevel = level ();
				experience = + experienceToAdd;
				float possiblyAnotherLevel = level ();
				bool didLevelUp = currentLevel < possiblyAnotherLevel;
				
				if (didLevelUp) {
						Debug.Log ("Did level up to level.... " + possiblyAnotherLevel);
				}
		}
		
		public float level ()
		{
				float level = 0;
				if (experience < levelDivider) {
						level = 0;
				} else if (experience < 3 * levelDivider) {
						level = 1;
				} else if (experience < 6 * levelDivider) {
						level = 2;
				} else if (experience < 10 * levelDivider) {
						level = 3;
				} else if (experience < 15 * levelDivider) {
						level = 4;
				} else if (experience < 21 * levelDivider) {
						level = 5;
				} else if (experience < 28 * levelDivider) {
						level = 6;
				}
				return level;
		}
}
