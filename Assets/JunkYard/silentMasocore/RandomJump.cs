using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RandomJump : MonoBehaviour {

	public AnimationCurve jumpArc;
	public int rerollAfterNumJumps;
	int jumpCount;
	bool jumping;
	public Transform lastCheckPoint;
	MicInput mic;
	Vector3 startPos;
	RawImage performance;
	// Use this for initialization
	void Start () {
		startPos = transform.position;
		jumping = false;
		jumpArc.keys = rerollJump();
		jumpCount = 0;
		performance = gameObject.GetComponentInChildren<RawImage>();
		mic = GetComponent<MicInput>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetAxis("Horizontal") != 0) {
			transform.Translate(new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * 3, 0 , 0));
		}
		if(Input.GetKeyDown(KeyCode.Space) && !jumping) {
			StartCoroutine(Jump());
			jumpCount++;
		}

		if(Input.GetKeyDown(KeyCode.R)){
			jumpArc.keys = rerollJump();
		}
		if(mic.MicLoudness > 0.05f){
			transform.position = startPos;
			performance.gameObject.SetActive(true);
		}
		else {
			performance.gameObject.SetActive(false);
		}
	}

	Keyframe[] rerollJump() {
		Keyframe[] temp = new Keyframe[4];
		temp[0] = new Keyframe(0, 0);
		temp[1] = new Keyframe(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
		temp[2] = new Keyframe(Random.Range(temp[1].time, 1.0f), Random.Range(0.0f, 1.0f));
		temp[3] = new Keyframe(0.99f, 0.01f);

		return temp;
	}

	IEnumerator Jump(){
		float t = 0;
		jumping = true;
		float startingY = transform.position.y;
		while(t < 1) {
			if(!jumping) {
				break;
			}
			transform.Translate(new Vector3(0f, (jumpArc.Evaluate(t) * 0.5f), 0));
			t+= Time.deltaTime;
			yield return null;
		}
		if(jumpCount >= rerollAfterNumJumps) {
			jumpArc.keys = rerollJump();
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		jumping = false;
	}
}