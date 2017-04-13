using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuPlatformerEnemy : MonoBehaviour {

	public enum EnemyType {Ground, Mid, Air};

	public EnemyType myType;

	// Use this for initialization
	void Start () {
		if(myType == EnemyType.Air) {
			GetComponent<Rigidbody2D>().gravityScale = 0;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Player") {
			Debug.Log("wat");
			if(!other.gameObject.GetComponent<MenuBasedPlatformerMovement>().blocking) {
				SceneManager.LoadScene(0);	
			}
		}
		else {
			Debug.Log(other.gameObject.name);
		}
	}
}
