using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlatformerDeath : MonoBehaviour {

	public float freezeTime;
	AudioSource audio;
	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();
		if(freezeTime < audio.clip.length) {
			freezeTime = audio.clip.length;
		}
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D other) {
		if(other.tag == "Player") {
			audio.Play();
			StartCoroutine(DeathFreeze());
		}
	}

	IEnumerator DeathFreeze() {
		float startTime = Time.realtimeSinceStartup;
		Time.timeScale = 0;
		while(Time.realtimeSinceStartup < startTime + freezeTime) {
			yield return null;
		}
		Time.timeScale = 1;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
