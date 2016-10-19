using UnityEngine;
using System.Collections;

public class GemBehavior : MonoBehaviour {

	public float FloatAmount;
	private float floated;
	// Use this for initialization
	void Start () {
		floated = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(FloatAmount > 0) {
			floated += (Time.deltaTime / 5);
		}
		else {
			floated -= (Time.deltaTime / 5);
		}
		if(Mathf.Abs(floated) > Mathf.Abs(FloatAmount)) {
			FloatAmount *= -1;
			floated = 0;
		}
		transform.position += new Vector3(0f, floated, 0f);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Player") {
			Debug.Log("You win!");
		}
	}
}
