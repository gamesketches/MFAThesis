using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

	public Vector2 movement;
	public float speed;
	private Vector3 travelDistance;
	private Vector3 curMovementVector;
	// Use this for initialization
	void Start () {
		if(movement.y == 0) {
			curMovementVector = new Vector3(speed, 0, 0);
		}
		else {
			curMovementVector = new Vector3(0, speed, 0);
		}
		travelDistance = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += curMovementVector * Time.deltaTime;
		travelDistance += curMovementVector * Time.deltaTime;
		if(travelDistance.magnitude > movement.magnitude) {
			travelDistance = Vector3.zero;
			curMovementVector *= -1;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Player"){
			other.transform.parent = transform;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if(other.tag == "Player") {
			other.transform.parent = null;
		}
	}
}
