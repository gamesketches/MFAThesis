using UnityEngine;
using System.Collections;

public class AnemoneBehavior : MonoBehaviour {

	public bool consuming;
	// Use this for initialization
	void Start () {
		consuming = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "egg" && !consuming) {
			consuming = true;
			GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("FullAnemone");
			Destroy(other.gameObject);
			Instantiate(Resources.Load<GameObject>("prefabs/BubbleParticles"), transform.position, Quaternion.identity);
		}
	}

}
