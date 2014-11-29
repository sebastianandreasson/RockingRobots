using UnityEngine;
using System.Collections;

using Pose = Thalmic.Myo.Pose;

public class WeaponLogicScript : MonoBehaviour
{
		protected Animator animator;
		GameObject objectThatWeArePullingTowardsUs;
		bool isCurrentlyPulling;
		bool isHoldingProjectile;
		bool isHoldingFist;
		public float pullForce;
		public float pushForce;
	
		public float acceptableGrabDistance;
		
		public GameObject arm;
		public GameObject myo = null;
		// Use this for initialization
		void Start ()
		{
				animator = arm.GetComponent<Animator> ();
		}
	
		// Update is called once per frame
		void Update ()
		{
				ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo> ();
				GameObject objectThatWeArePointingAt = objectInWeaponsDirection ();
				Projectile projectileScript = null;
				if (objectThatWeArePointingAt) {
						projectileScript = objectThatWeArePointingAt.GetComponent<Projectile> ();
				}
				
				bool isLookingAtProjectile = projectileScript != null;
				//Debug.Log ("isLookingAtProj:: " + isLookingAtProjectile);
				//Debug.Log (projectileScript);
				if (projectileScript) {
						//this was a projectile!
						//tell it that we pointed to it.
						projectileScript.playerIsAimingAtThisObject ();
				}
                
            	
				if (thalmicMyo.pose == Pose.Fist || (thalmicMyo.pose == Pose.Rest && isHoldingFist)) {
						isHoldingFist = true;
						animator.SetTrigger ("close");
				} else if (thalmicMyo.pose != Pose.Fist && thalmicMyo.pose != Pose.Rest) {
						isHoldingFist = false;
						animator.SetTrigger ("open");
				} else if (!isHoldingProjectile) {
						isHoldingFist = false;
						animator.SetTrigger ("open");
				}
            
				bool shouldActivePull = isHoldingFist && isLookingAtProjectile;
				bool shouldDeactivePull = !isHoldingFist && isCurrentlyPulling;
				bool shouldPush = !isHoldingFist && isHoldingProjectile;
				
//				Debug.Log ("shouldActivate:: " + shouldActivePull);
//				Debug.Log ("shouldDeActivate:: " + shouldDeactivePull);
//				Debug.Log ("shouldPush:: " + shouldPush);
//				Debug.Log ("MouseUp::: " + Input.GetMouseButtonUp (0));
//				Debug.Log ("isholding:: " + isHoldingProjectile);
            
				Debug.Log ("Pose: " + thalmicMyo.pose);
				Debug.Log ("isHolding: " + isHoldingFist);
		
				
				
				if (shouldDeactivePull) {
						isCurrentlyPulling = false;
				}
		
				if (shouldActivePull) {
						isCurrentlyPulling = true;
						objectThatWeArePullingTowardsUs = objectThatWeArePointingAt;
				}
				
				if (isCurrentlyPulling) {
						//decide if we should stop
				
						Vector3 distanceVector = gameObject.transform.position - objectThatWeArePullingTowardsUs.transform.position;
						float distanceFromTarget = distanceVector.magnitude;
						if (distanceFromTarget < acceptableGrabDistance) {
								isCurrentlyPulling = false;
								isHoldingProjectile = true;
//								objectThatWeArePullingTowardsUs.transform.position = transform.position;
								objectThatWeArePullingTowardsUs.transform.rigidbody.velocity = new Vector3 (0, 0, 0);
								objectThatWeArePullingTowardsUs.transform.parent = transform;	
								objectThatWeArePullingTowardsUs.rigidbody.useGravity = false;
//                                objectThatWeArePullingTowardsUs.transform.position = new Vector3(0,0,10.0);
								objectThatWeArePullingTowardsUs.rigidbody.constraints = RigidbodyConstraints.FreezePosition;
						}
			
				}
		
				if (isCurrentlyPulling) {
						Vector3 directionToFly = gameObject.transform.position - objectThatWeArePullingTowardsUs.transform.position;
						directionToFly.Normalize ();
						objectThatWeArePullingTowardsUs.rigidbody.AddForce (directionToFly * pullForce);
				}
				
				if (shouldPush) {
						isHoldingProjectile = false;
						objectThatWeArePullingTowardsUs.rigidbody.constraints = RigidbodyConstraints.None;
						objectThatWeArePullingTowardsUs.transform.rigidbody.AddForce (transform.parent.transform.forward * pushForce);
						objectThatWeArePullingTowardsUs.transform.parent = null;
						objectThatWeArePullingTowardsUs.rigidbody.useGravity = true;
				}
				
		}
	
		GameObject objectInWeaponsDirection ()
		{
				RaycastHit hit;
				float thickness = 2f;
				Vector3 origin = transform.position + new Vector3 (0, 0.6f, -1.6f);
				Vector3 direction = transform.TransformDirection (Vector3.forward);
				GameObject gameObjectThatWasHit = null;
				if (Physics.SphereCast (origin, thickness, direction, out hit)) {
						Vector3 positionOfObjectThatWasHit = hit.collider.gameObject.transform.position;
						gameObjectThatWasHit = hit.collider.gameObject;
				}
				return gameObjectThatWasHit;
//				RaycastHit hit;
//				GameObject gameObjectThatWasHit = null;
//				if (Physics.Raycast (transform.position, transform.parent.transform.forward, out hit)) {
//						Vector3 positionOfObjectThatWasHit = hit.collider.gameObject.transform.position;
//						//Debug.Log ("found object hit! vector: " + positionOfObjectThatWasHit);
//						gameObjectThatWasHit = hit.collider.gameObject;
////						Debug.Log(gameObjectThatWasHit.transform.scale);
//						//Debug.DrawRay (transform.position, transform.parent.transform.forward);
//				}
//				return gameObjectThatWasHit;
		}
}
