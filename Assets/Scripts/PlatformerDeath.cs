using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlatformerDeath : MonoBehaviour {

	public float freezeTime;
	AudioSource audioSource;
	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
		if(freezeTime < audioSource.clip.length) {
			freezeTime = audioSource.clip.length;
		}
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D other) {
		if(other.tag == "Player") {
			audioSource.Play();
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
		Debug.Log("ha");
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
