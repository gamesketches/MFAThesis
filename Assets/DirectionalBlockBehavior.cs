using UnityEngine;
using System.Collections;

public class DirectionalBlockBehavior : MonoBehaviour {

	public Direction direction;
	Rigidbody2D rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		if(direction == Direction.Up || direction == Direction.Down) {
			rb.constraints = RigidbodyConstraints2D.FreezePositionX;
		}
		else {
			rb.constraints = RigidbodyConstraints2D.FreezePositionY;
		}
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 curVelocity = rb.velocity;
		if(direction == Direction.Up) {
			if(curVelocity.y < 0) {
				rb.velocity = Vector2.zero;
			}
		}
	}
}
