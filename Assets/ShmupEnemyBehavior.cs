using UnityEngine;
using System.Collections;

public class ShmupEnemyBehavior : MonoBehaviour {

	AnimationCurve travelArc;
	public RectTransform viewPort;
	private float timer;
	public float verticalSpeed;
	// Use this for initialization
	void Start () {
		SetAnimationCurve(new Keyframe[3]{new Keyframe(0, 0), new Keyframe(0.5f, 0.5f), new Keyframe(1, 1)});
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		transform.Translate(0, -verticalSpeed * Time.deltaTime, 0);
		Vector3 temp = transform.position;
		temp.x = travelArc.Evaluate(timer) * 20 + 20;//viewPort.rect.width;
		transform.position = temp;
	}

	public void SetAnimationCurve(Keyframe[] points) {
		travelArc = new AnimationCurve(points);
	}

}
