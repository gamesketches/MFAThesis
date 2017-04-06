using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalArea : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Player") {
			GetComponent<ParticleSystem>().Play();
			GetComponent<AudioSource>().Play();
			Invoke("ResetScene", GetComponent<AudioSource>().clip.length);
		}
	}

	void ResetScene() {
		SceneManager.LoadScene("MenuBasedPlatformer");
	}
}
