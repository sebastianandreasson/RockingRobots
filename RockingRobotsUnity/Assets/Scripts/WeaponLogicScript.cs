using UnityEngine;
using System.Collections;

using Pose = Thalmic.Myo.Pose;

public class WeaponLogicScript : MonoBehaviour
{
		GameObject objectThatWeArePullingTowardsUs;
		bool isCurrentlyPulling;
		bool isHoldingProjectile;
		public float pullForce;
		public float pushForce;
	
		public float acceptableGrabDistance;
		
        public GameObject myo = null;
        private Pose _lastPose = Pose.Unknown;
		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
                ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo> ();    
				GameObject objectThatWeArePointingAt = objectInWeaponsDirection ();
				Projectile projectileScript = objectThatWeArePointingAt.GetComponent<Projectile> ();
				bool isLookingAtProjectile = projectileScript != null;
				Debug.Log ("isLookingAtProj:: " + isLookingAtProjectile);
				Debug.Log (projectileScript);
				if (projectileScript) {
						//this was a projectile!
						//tell it that we pointed to it.
						projectileScript.playerIsAimingAtThisObject ();
				}
            
                bool isHolding = false;    
                if (thalmicMyo.pose == Pose.Fist || (thalmicMyo.pose == Pose.Rest && _lastPose == Pose.Fist)) {
                    isHolding = true;
                    _lastPose = thalmicMyo.pose;
                }
                else if ((thalmicMyo.pose != Pose.Rest && thalmicMyo.pose != Pose.Fist) && _lastPose == Pose.Fist){
                    isHolding = false;
                    _lastPose = thalmicMyo.pose;
                }
            
				bool shouldActivePull = isHolding && isLookingAtProjectile;
				bool shouldDeactivePull = isHolding && isCurrentlyPulling;
				bool shouldPush = !isHolding && isHoldingProjectile;
				
				Debug.Log ("shouldActivate:: " + shouldActivePull);
				Debug.Log ("shouldDeActivate:: " + shouldDeactivePull);
				Debug.Log ("shouldPush:: " + shouldPush);
				Debug.Log ("MouseUp::: " + Input.GetMouseButtonUp (0));
				Debug.Log ("isholding:: " + isHoldingProjectile);
		
				
				
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
								objectThatWeArePullingTowardsUs.transform.position = transform.position;
								objectThatWeArePullingTowardsUs.transform.rigidbody.velocity = new Vector3 (0, 0, 0);
								objectThatWeArePullingTowardsUs.transform.parent = transform;	
						}
			
				}
		
				if (isCurrentlyPulling) {
						objectThatWeArePullingTowardsUs.rigidbody.useGravity = false;
						Vector3 directionToFly = gameObject.transform.position - objectThatWeArePullingTowardsUs.transform.position;
						directionToFly.Normalize ();
						objectThatWeArePullingTowardsUs.rigidbody.AddForce (directionToFly * pullForce);
				}
				
				if (shouldPush) {
						isHoldingProjectile = false;
						objectThatWeArePullingTowardsUs.transform.rigidbody.AddForce (transform.parent.transform.forward * pushForce);
						objectThatWeArePullingTowardsUs.transform.parent = null;
						objectThatWeArePullingTowardsUs.rigidbody.useGravity = true;
				}
				
		}
	
		GameObject objectInWeaponsDirection ()
		{
				RaycastHit hit;
				GameObject gameObjectThatWasHit = null;
				if (Physics.Raycast (transform.position, transform.parent.transform.forward, out hit)) {
						Vector3 positionOfObjectThatWasHit = hit.collider.gameObject.transform.position;
						//Debug.Log ("found object hit! vector: " + positionOfObjectThatWasHit);
						gameObjectThatWasHit = hit.collider.gameObject;
						
						//Debug.DrawRay (transform.position, transform.parent.transform.forward);
				}
				return gameObjectThatWasHit;
		}
}
