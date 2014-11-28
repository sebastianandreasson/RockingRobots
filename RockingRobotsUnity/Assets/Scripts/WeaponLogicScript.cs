using UnityEngine;
using System.Collections;

public class WeaponLogicScript : MonoBehaviour
{

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
				
				GameObject objectThatWeArePointingAt = objectInWeaponsDirection ();
				Projectile projectileScript = objectThatWeArePointingAt.GetComponent<Projectile> ();
				if (projectileScript) {
						//this was a projectile!
						//tell it that we pointed to it.
						projectileScript.playerIsAimingAtThisObject ();
				}
		}
	
		GameObject objectInWeaponsDirection ()
		{
				RaycastHit hit;
				GameObject gameObjectThatWasHit = null;
				if (Physics.Raycast (transform.position, transform.parent.transform.forward, out hit)) {
						float distanceToGround = hit.distance;
						Vector3 positionOfObjectThatWasHit = hit.collider.gameObject.transform.localPosition;
						Debug.Log ("found object hit! vector: " + positionOfObjectThatWasHit);
						gameObjectThatWasHit = hit.collider.gameObject;
				}
				return gameObjectThatWasHit;
		}
}
