using UnityEngine;
using System.Collections;

public class AnemoneBehavior : MonoBehaviour {

	public bool consuming;
	// Use this for initialization
	void Start () {
		consuming = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "egg") {
			consuming = true;
			Debug.Log("EGG ACCEPTED");
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if(other.tag == "egg") {
			consuming = false;
			Debug.Log("EGG LEFT");
		}
	}
}
