using UnityEngine;
using System.Collections;

public class WalkingScript : MonoBehaviour
{
		public float timeBetweenDirectionChanges;
		float timeForNextDirectionChange;
	
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
				target = GameObject.Find ("Player").transform;
				Debug.Log ("player!:" + target);
		}
	
		// Update is called once per frame
		void Update ()
		{
		
		
				if (Time.time > timeForNextDirectionChange) {
//						Debug.Log ("want to change direction");
						Vector3 direction = target.transform.position - transform.position;
						float distanceBetween = direction.magnitude;
			
						//direction.Normalize ();
						transform.LookAt (target.position);
						//						if (maxDistanceWeCare > distanceBetween && distanceBetween > minimuDistanceForAccurateWalking) {
						//								//add a random direction
						//								float randomDirection = Random.Range (-30, 30);
						//								transform.forward = Quaternion.AngleAxis (randomDirection, Vector3.up) * transform.forward;
						//				
						//						}
						//						Vector3 newTarget = myTransform.position + direction * distanceBetween;
						//						myTransform.rotation = Quaternion.Slerp (myTransform.rotation, Quaternion.LookRotation (newTarget - myTransform.position), rotationSpeed * Time.deltaTime);
						timeForNextDirectionChange = Time.time + timeBetweenDirectionChanges;
				}
		
				rigidbody.velocity = moveSpeed * transform.forward;
		
		}
	
}
