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
	
		}
	
		GameObject objectInWeaponsDirection ()
		{
				RaycastHit hit;
				if (Physics.Raycast (transform.position, transform.parent.transform.forward, out hit)) {
						float distanceToGround = hit.distance;
						Vector3 positionOfObjectThatWasHit = hit.collider.gameObject.transform.localPosition;
				}
				return hit.collider.gameObject;
		}
}
