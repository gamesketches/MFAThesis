using UnityEngine;
using System.Collections;

public class WhirlPoolExit : MonoBehaviour {

	public DirectionalSlot[] slots;
	public Vector3 targetPos;
	public Transform targetRoom;
	public Vector3 targetRotation;
	SpriteRenderer renderer;
	// Use this for initialization
	void Start () {
		renderer = GetComponent<SpriteRenderer>();
		renderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		foreach(DirectionalSlot slot in slots) {
			if(!slot.occupied) {
				return;
			}
		}
		renderer.enabled = true;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Player" && renderer.enabled) {
			other.transform.position = new Vector3(targetPos.x, targetPos.y, 0);
			targetPos = new Vector3(targetRoom.transform.position.x, targetRoom.transform.position.y, -10);
			StartCoroutine(ChangeRoom());
		}
	}

	IEnumerator ChangeRoom() {
		float t = 0;
		Transform compass = GameObject.Find("Compass").transform;
		Vector3 startPos = Camera.main.transform.position;
		Quaternion startRot = Camera.main.transform.rotation;
		Quaternion targetRot = Quaternion.Euler(targetRotation);
		Quaternion startCompassRot = compass.rotation;
		Quaternion targetCompassRot = Quaternion.Euler(targetRotation.x, targetRotation.y, -targetRotation.z);
		while(t < 1) {
			Camera.main.transform.position = Vector3.Lerp(startPos, targetPos, t);
			Camera.main.transform.rotation = Quaternion.Lerp(startRot, targetRot, t);
			compass.rotation = Quaternion.Lerp(startCompassRot, targetCompassRot, t);
			t += Time.deltaTime;
			yield return null;
		}
		Camera.main.transform.position = targetPos;
		Camera.main.transform.rotation = targetRot;
		transform.parent.gameObject.GetComponentInChildren<whirlPoolBehavior>().active = false;
	}
}
