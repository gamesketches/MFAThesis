using UnityEngine;
using System.Collections;

public class DirectionalBlockCollider : MonoBehaviour {

	public bool colliding;
	public bool coreDirection;
	// Use this for initialization
	void Awake () {
		colliding = false;
		coreDirection = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Player" && coreDirection){
			colliding = true;
			}
		else if(other.tag == "directionBlock") {		
			colliding = true;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		colliding = false;
	}
}
