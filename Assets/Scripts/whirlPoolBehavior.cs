using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class whirlPoolBehavior : MonoBehaviour {

	Transform player;
	public float pullSpeed;
	public Rect activeSize;
	BoxCollider2D collider;
	public bool active = false;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		collider = GetComponent<BoxCollider2D>();
		if(activeSize.position == Vector2.zero && activeSize.size == Vector2.zero) {
			activeSize.position = collider.offset;
			activeSize.size = collider.size;
		}
		Rect temp = new Rect(collider.offset, collider.size);
		collider.offset = activeSize.position;
		collider.size = activeSize.size;
		activeSize = temp;
	}
	
	// Update is called once per frame
	void Update () {
		if(active) {
			transform.Rotate(Vector3.forward, 1);
			player.Translate( CalculatePull());
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Player") {
			if(active) {
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			}
			else {
				collider.offset = activeSize.position;
				collider.size = activeSize.size;
			}
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if(other.tag == "Player") {
			if(active) {
				active = false;
			}
			else {
				active = true;
			}
		}
	}

	Vector3 CalculatePull() {
		Vector3 diff = player.position - transform.position;
		float xVal = diff.x > 1 || diff.x < -1 ? pullSpeed / -diff.x : 0;
		float yVal = diff.y > 1 || diff.y < -1 ? pullSpeed / -diff.y : 0;
		return new Vector3(xVal, yVal, 0);
	}
}
