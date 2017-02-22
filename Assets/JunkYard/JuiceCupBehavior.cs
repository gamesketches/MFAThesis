using UnityEngine;
using System.Collections;

public class JuiceCupBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D other) {
		Debug.Log("That's some tasty juice");
	}
}
