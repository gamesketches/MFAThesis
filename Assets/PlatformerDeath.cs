using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlatformerDeath : MonoBehaviour {

	public float freezeTime;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D other) {
		if(other.tag == "Player") {
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
