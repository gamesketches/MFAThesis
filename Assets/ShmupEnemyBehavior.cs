using UnityEngine;
using System.Collections;

public class ShmupEnemyBehavior : MonoBehaviour {

	AnimationCurve travelArc;
	public RectTransform viewPort;
	private float timer;
	public float verticalSpeed;
	public bool caught;
	// Use this for initialization
	void Start () {
		caught = false;
		SetAnimationCurve(new Keyframe[3]{new Keyframe(0, 0), new Keyframe(0.5f, 0.5f), new Keyframe(1, 1)});
	}
	
	// Update is called once per frame
	void Update () {
		if(!caught) {
			timer += Time.deltaTime;
			transform.Translate(0, -verticalSpeed * Time.deltaTime, 0);
			Vector3 temp = transform.position;
			temp.x = travelArc.Evaluate(timer) * 20 + 20;//viewPort.rect.width;
			transform.position = temp;
		}
	}

	public void SetAnimationCurve(Keyframe[] points) {
		travelArc = new AnimationCurve(points);
	}

	public float Squeeze() {
		Debug.Log("Squeezin");
		Vector3 startingScale = transform.lossyScale;
		transform.localScale = new Vector3(startingScale.x * 0.9f, startingScale.y * 1, startingScale.z * 1);
		return transform.lossyScale.x;
	}
}
