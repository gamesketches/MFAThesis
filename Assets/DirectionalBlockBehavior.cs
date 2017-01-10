using UnityEngine;
using System.Collections;

public class DirectionalBlockBehavior : MonoBehaviour {

	public Direction direction;
	Rigidbody2D rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		SetConstraints();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag == "directionBlock") {
			rb.constraints = RigidbodyConstraints2D.None;
			Debug.Log("removing constraints");
			Debug.Log(other.contacts[0].point.y < transform.position.y);
		}
		else {
			Debug.Log(other.gameObject.tag);
		}
	}

	void OnCollisionExit2D(Collision2D other) {
		if(other.gameObject.tag == "directionBlock") {
			SetConstraints();
			Debug.Log("reenabling constraints");	
		}
	}

	void SetConstraints(){
		if(direction == Direction.Up || direction == Direction.Down) {
			rb.constraints = RigidbodyConstraints2D.FreezePositionX;
		}
		else {
			rb.constraints = RigidbodyConstraints2D.FreezePositionY;
		}
	}
}
