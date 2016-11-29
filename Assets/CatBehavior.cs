using UnityEngine;
using System.Collections;

public class CatBehavior : MonoBehaviour {

	Animator controller;
	private enum CatState {Idle, Walk, Sleep, Roll};
	public float stateChangeTimeRange;
	private float timer;
	private CatState state;
	Rigidbody2D rb;
	UnityStandardAssets._2D.Camera2DFollow followScript;

	// Use this for initialization
	void Start () {
		controller = GetComponent<Animator>();
		controller.speed = 0.6f;
		state = CatState.Idle;
		timer = Random.Range(0, stateChangeTimeRange);
		rb = GetComponent<Rigidbody2D>();
		followScript = Camera.main.GetComponent<UnityStandardAssets._2D.Camera2DFollow>();
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if(timer < 0) {
			timer = Random.Range(3, stateChangeTimeRange);
			state =  (CatState)System.Enum.GetValues(typeof(CatState)).GetValue(Random.Range(0, 4));
			controller.SetInteger("catState", (int) state);
			StartCoroutine(EyesOnMe(transform));
		}
		switch(state) {
			case CatState.Walk: 
				controller.speed = 0.6f;
				rb.AddForce(new Vector2(1, 0) * Time.deltaTime);
				Camera.main.transform.rotation = Quaternion.identity;
				transform.parent = null;
				break;
			case CatState.Roll:
				controller.speed = 0.5f;
				Camera.main.transform.rotation = Quaternion.Euler(0, 0, 15);
				transform.parent = null;
				break;
			default:
				controller.speed = 0.1f;
				Camera.main.transform.rotation = Quaternion.identity;
				break;
		}
	}

	IEnumerator EyesOnMe(Transform target) {
		float t = 0;
		Vector3 startPos = Camera.main.transform.position;
		Vector3 targetPos = target.position;
		targetPos.z = startPos.z;
		if(state == CatState.Roll) {
			while(t < 0) {
				Camera.main.transform.position = Vector3.Lerp(startPos, targetPos, t);
				t += Time.deltaTime;
				yield return null;
			}
			followScript.target = target;
		}
		else {
			while(t < 0.5) {
				Camera.main.transform.position = Vector3.Lerp(startPos, targetPos, t);
				t += Time.deltaTime;
				yield return null;
			}
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		if(other.tag == "Player") {
			transform.parent = other.transform;
			state = CatState.Sleep;
			timer = Random.Range(3, stateChangeTimeRange);
		}
	}
}
