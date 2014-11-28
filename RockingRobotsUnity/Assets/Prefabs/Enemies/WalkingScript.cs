using UnityEngine;
using System.Collections;

public class WalkingScript : MonoBehaviour
{
	
	
		public GameObject targetPoint;
		public float moveSpeed;
		public float rotationSpeed;
	
		public float minimuDistanceForAccurateWalking;
		public float maxDistanceWeCare;
	
		private Transform myTransform;
		private Transform target;
	
		// Use this for initialization
		void Start ()
		{
				myTransform = gameObject.transform;
		}
	
		// Update is called once per frame
		void Update ()
		{
				target = targetPoint.transform;
				//              myTransform.rotation = Quaternion.Slerp (myTransform.rotation, Quaternion.LookRotation (target.position - myTransform.position), rotationSpeed * Time.deltaTime);
		
				//      
		
		
				Vector3 direction = target.transform.position - transform.position;
				float distanceBetween = direction.magnitude;
		
				if (maxDistanceWeCare > distanceBetween && distanceBetween > minimuDistanceForAccurateWalking) {
						//add a random direction
						float randomDirection = Random.Range (-45, 45);
						direction = Quaternion.AngleAxis (randomDirection, Vector3.up) * direction;
			
				}
				direction.Normalize ();
				Vector3 newTarget = myTransform.position + direction * distanceBetween;
				myTransform.rotation = Quaternion.Slerp (myTransform.rotation, Quaternion.LookRotation (newTarget - myTransform.position), rotationSpeed * Time.deltaTime);
		
				//direction.Normalize ();
				rigidbody.velocity = moveSpeed * direction;
				//transform.LookAt (target.transform);
		
				Debug.Log ("direction is: " + direction);
		
				//              myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
		}
	
}
