using UnityEngine;
using System.Collections;

public class RockScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnCollisionEnter(Collision other){
		if (!audio.isPlaying){
			audio.Play();
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}
