using UnityEngine;
using System.Collections;

public class ShmupProjectileBehavior : MonoBehaviour {

	public float lifeTime;
	public float speed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		lifeTime -= Time.deltaTime;
		if(lifeTime <= 0) {
			Destroy(gameObject);
		}
		transform.Translate(0, speed * Time.deltaTime, 0);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag != "Player") {
			Destroy(other.gameObject);
			Destroy(gameObject);
		}
	}
}
