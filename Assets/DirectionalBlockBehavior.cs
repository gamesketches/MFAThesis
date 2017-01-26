using UnityEngine;
using System.Collections;

public class DirectionalBlockBehavior : MonoBehaviour {

	public Direction direction;
	Rigidbody2D rb;
	DirectionalBlockCollider[] colliders;
	Vector2[] directionVector = {new Vector2(0, 1),
								new Vector2 (0, -1),
								new Vector2 (-1, 0),
								 new Vector2(1, 0)};
	bool moving;
	// Use this for initialization
	void Start () {
		moving = false;
		colliders = GetComponentsInChildren<DirectionalBlockCollider>();
		colliders[(int)direction].coreDirection = true;
		rb = GetComponent<Rigidbody2D>();
		SpriteRenderer renderer = GetComponent<SpriteRenderer>();
		switch(direction) {
			case Direction.Up:
				renderer.color = Color.blue;
				break;
			case Direction.Down:
				renderer.color = Color.red;
				break;
			case Direction.Left:
				renderer.color = Color.white;
				break;
			case Direction.Right:
				renderer.color = Color.green;
				break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		foreach(DirectionalBlockCollider collider in colliders) {
			if(!moving && collider.colliding) {
			StartCoroutine(Move(directionVector[collider.transform.GetSiblingIndex()]));
		}

		}
	}

	/*void OnCollisionEnter2D(Collision2D other) {
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
	}*/

	void SetConstraints(){
		if(direction == Direction.Up || direction == Direction.Down) {
			rb.constraints = RigidbodyConstraints2D.FreezePositionX;
		}
		else {
			rb.constraints = RigidbodyConstraints2D.FreezePositionY;
		}
	}

	IEnumerator Move(Vector2 directionVector) {
		moving = true;
		Vector3 movementVector = new Vector3(directionVector.x, directionVector.y, 0);
		Vector3 startPos = transform.position;
		Vector3 endPos = startPos + movementVector;
		float t = 0;
		while(t < 1) {
			transform.position = Vector3.Lerp(startPos, endPos, t);
			t += Time.deltaTime;
			yield return null;
		}

		colliders[(int)direction].colliding = false;
		moving = false;
	}

}
