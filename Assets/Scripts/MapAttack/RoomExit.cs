using UnityEngine;
using System.Collections;

public class RoomExit : MonoBehaviour {

	public Direction dir;


	public GameObject[] anemones;

	// Use this for initialization
	void Start () {
		anemones = GameObject.FindGameObjectsWithTag("anemone");
	}

	void Update() {
		anemones = GameObject.FindGameObjectsWithTag("anemone");
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		foreach(GameObject anemone in anemones) {
			Debug.Log(anemone.name);
			if(anemone.activeInHierarchy && !anemone.GetComponent<AnemoneBehavior>().consuming) {
				return;
			} 
		}
		other.transform.position = Vector3.zero;
		other.GetComponent<MappedController>().RemappControls(dir);
	}
}
