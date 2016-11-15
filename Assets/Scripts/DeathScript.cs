using UnityEngine;
using System.Collections;

public class DeathScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D other) {
		if(other.tag == "Player"){
			other.transform.position = other.GetComponent<RandomJump>().lastCheckPoint.transform.position;
		}
	}
}
