using UnityEngine;
using System.Collections;

public class CubeGrab : MonoBehaviour {

	public float lifeTime;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.parent != null) {
			lifeTime -= Time.deltaTime;
			if(lifeTime <= 0) {
				Camera.main.GetComponent<UnityStandardAssets._2D.Camera2DFollow>().target = GameObject.FindGameObjectWithTag("Player").transform;
				Destroy(gameObject);
			}
		}
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Player"){
			transform.parent = other.transform;
		}
	}
}
