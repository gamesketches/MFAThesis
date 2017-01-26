using UnityEngine;
using System.Collections;

public class AttackHitBoxManager : MonoBehaviour {

	public float duration;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		duration -= Time.deltaTime;
		if(duration <= 0) {
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "PlatformerEnemy") {
			Destroy(other.gameObject);
		}
	}
}
