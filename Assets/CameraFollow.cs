using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	Rigidbody2D playerRB;
	Vector3 lastFramePosition;
	// Use this for initialization
	void Start () {
		playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
		lastFramePosition = playerRB.position;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 position = transform.position;
		position.x += playerRB.position.x - lastFramePosition.x;
		transform.position = position;
		lastFramePosition = playerRB.position;
	}
}
