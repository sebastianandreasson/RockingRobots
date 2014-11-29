using UnityEngine;
using System.Collections;

using Pose = Thalmic.Myo.Pose;

public class WeaponLogicScript : MonoBehaviour
{

		LevelingScript playerLevelingScript;
	
		protected Animator animator;
		GameObject objectThatWeArePullingTowardsUs;
		bool isCurrentlyPulling;
		bool isHoldingProjectile;
		bool isHoldingFist;
		public float pullForce = 5000;
		public float pushForce = 9000;
	public float maxPullVelocity = 100;
	
		public float acceptableGrabDistance = 10;
		
		public GameObject arm;
		public GameObject myo = null;
		// Use this for initialization
		void Start ()
		{
				animator = arm.GetComponent<Animator> ();
				playerLevelingScript = GameObject.Find ("Player").GetComponent<LevelingScript> ();
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
				bool canWePickUpObject = false;
				if (projectileScript) {
						//this was a projectile!
						//tell it that we pointed to it.
						canWePickUpObject = projectileScript.playerIsAimingAtThisObjectWithLevelCanWePickUp (playerLevelingScript.level ());
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
				
<<<<<<< HEAD
		isHoldingFist = Input.GetMouseButtonDown (0) || Input.GetMouseButton(0);
=======
				isHoldingFist = Input.GetMouseButtonDown (0);
>>>>>>> 80ae8c21b32b80e77d4a0acbecb8e27799ebd383
            
				bool shouldActivePull = isHoldingFist && isLookingAtProjectile && canWePickUpObject;
				bool shouldDeactivePull = !isHoldingFist && isCurrentlyPulling;
				bool shouldPush = !isHoldingFist && isHoldingProjectile;
//				
//				Debug.Log ("shouldActivate:: " + shouldActivePull);
//				Debug.Log ("shouldDeActivate:: " + shouldDeactivePull);
//				Debug.Log ("shouldPush:: " + shouldPush);
//				Debug.Log ("MouseUp::: " + Input.GetMouseButtonUp (0));
//				Debug.Log ("isholding:: " + isHoldingProjectile);
//            
//				Debug.Log ("Pose: " + thalmicMyo.pose);
//				Debug.Log ("isHolding: " + isHoldingFist);
		
				
				
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
			Debug.Log("distance to object:: " + distanceFromTarget);
						if (distanceFromTarget < acceptableGrabDistance) {
								isCurrentlyPulling = false;
				Debug.Log ("Bailing out bc of distance");				
				isHoldingProjectile = true;
//								objectThatWeArePullingTowardsUs.transform.position = transform.position;
								objectThatWeArePullingTowardsUs.transform.rigidbody.velocity = new Vector3 (0, 0, 0);
								objectThatWeArePullingTowardsUs.transform.parent = transform;	
								objectThatWeArePullingTowardsUs.rigidbody.useGravity = false;
//                                objectThatWeArePullingTowardsUs.transform.position = new Vector3(0,0,10.0);
								objectThatWeArePullingTowardsUs.rigidbody.constraints = RigidbodyConstraints.FreezePosition;
						}
			
				}
		
				if (isCurrentlyPulling && objectThatWeArePullingTowardsUs.rigidbody.velocity.magnitude < maxPullVelocity)
			{

						Vector3 directionToFly = gameObject.transform.position - objectThatWeArePullingTowardsUs.transform.position;
						directionToFly.Normalize ();
						objectThatWeArePullingTowardsUs.rigidbody.AddForce (directionToFly * pullForce);
			Debug.Log ("we are adding force" + directionToFly * pullForce);
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
