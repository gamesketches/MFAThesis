using UnityEngine;
using System.Collections;

public class RhythmicJump : MonoBehaviour {

	public int BPM;
	public float jumpHeight;
	public float jumpHelp;
	private float timeForBeat;
	public AnimationCurve jumpArc;
	private bool jumping;
	Rigidbody2D rb;
	// Use this for initialization
	void Start () {
		jumping = false;
		timeForBeat = (60000 / (float)BPM) / 1000; // 1 minute (60,000 ms) / BPM / 1000 to convert to seconds
		RecalculateJumpArc();
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			//StartCoroutine(Jump());
			rb.AddForce(new Vector2(0f, 8f), ForceMode2D.Impulse);
		}
	}

	IEnumerator Jump() {
		if(!jumping) {
			jumping = true;
			float t = 0;
			while(t < timeForBeat) {
				transform.position = new Vector3(transform.position.x, jumpArc.Evaluate(t), 0);
				t += Time.deltaTime;
				yield return null;
			}
			jumping = false;
		}
	}

	void RecalculateJumpArc() {
		jumpArc = new AnimationCurve();
		jumpArc.AddKey(new Keyframe(0.0f, 0f));
		jumpArc.AddKey(new Keyframe(0.0625f, jumpHeight));
		jumpArc.AddKey(new Keyframe(0.125f, 0.01f));
	}
}
