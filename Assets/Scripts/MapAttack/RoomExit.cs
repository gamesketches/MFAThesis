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

		other.gameObject.GetComponent<MappedController>().RemappControls(dir);
		StartCoroutine(RoomTransition(other.gameObject));
	}

	IEnumerator RoomTransition(GameObject player) {
		float t = 0;
		Vector3 targetPos = transform.position;
		Vector3 startPos = Camera.main.transform.position;
		float rotationAmount = 0;
		switch(dir) {
			case Direction.Up:
				rotationAmount = 0;
				break;
			case Direction.Left:
				rotationAmount = 90;
				break;
			case Direction.Right:
				rotationAmount = -90;
				break;
			case Direction.Down:
				rotationAmount = 180;
				break;
			default:
				Debug.LogError("Unknown Directional Enum");
				break;
		}
		Quaternion targetRotation = Quaternion.Euler(0, 0, rotationAmount);
		while(t < 1) {
			Camera.main.transform.position = Vector3.Lerp(startPos, targetPos, t);
			Camera.main.transform.rotation = Quaternion.Lerp(Quaternion.identity, targetRotation, t);
			Camera.main.fieldOfView = Mathf.Lerp(60, 1, t);
			t += Time.deltaTime;
			yield return null;
		}

		Camera.main.transform.position = new Vector3(0, 1, -10);
		Camera.main.transform.rotation = Quaternion.identity;
		Camera.main.fieldOfView = 60;
		startPos = new Vector3(0f, -3.5f, 0f);
		t = 0;
		while(t < 0.5) {
			player.transform.position = Vector3.Lerp(startPos, Vector3.zero, t);
			t += Time.deltaTime;
			yield return null;
		}
	}
}
