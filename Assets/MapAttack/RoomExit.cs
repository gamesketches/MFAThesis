using UnityEngine;
using System.Collections;

public class RoomExit : MonoBehaviour {

	public Direction dir;
	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		other.transform.position = Vector3.zero;
		other.GetComponent<MappedController>().RemappControls(dir);
	}
}
