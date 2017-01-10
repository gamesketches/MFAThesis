using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class whirlPoolBehavior : MonoBehaviour {

	Transform player;
	public float pullSpeed;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.forward, 1);
		player.Translate( CalculatePull());
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Player") {
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}

	Vector3 CalculatePull() {
		Vector3 diff = player.position - transform.position;
		Debug.Log(diff.x);
		float xVal = diff.x > 1 || diff.x < -1 ? pullSpeed / -diff.x : 0;
		float yVal = diff.y > 1 || diff.y < -1 ? pullSpeed / -diff.y : 0;
		return new Vector3(xVal, yVal, 0);
	}
}
