using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerTest : MonoBehaviour {

	float fingerDensity;
	LineRenderer finger;
	public AnimationCurve fingerAnimationCurve;
	EdgeCollider2D collider;
	// Use this for initialization
	void Start () {
		finger = GetComponent<LineRenderer>();
		fingerDensity = (float)transform.childCount;
		collider = GetComponent<EdgeCollider2D>();
		fingerAnimationCurve = new AnimationCurve();
		fingerAnimationCurve.AddKey(new Keyframe(0, 0));
		Vector2[] colliderPoints = new Vector2[transform.childCount];
		for(int i = 1; i < transform.childCount; i++) {
			fingerAnimationCurve.AddKey(new Keyframe(i , transform.GetChild(i - 1).localPosition.y));
			colliderPoints[i - 1] = new Vector2(transform.GetChild(i - 1).localPosition.x, transform.GetChild(i - 1).localPosition.y);
		}
		collider.points = colliderPoints;
		float k = 0;
		while(k < fingerDensity) {
			finger.numPositions = finger.numPositions + 1;
			finger.SetPosition(finger.numPositions - 1, new Vector3(k, fingerAnimationCurve.Evaluate(k)));
			k += Time.deltaTime;
		}
	}
	
	// Update is called once per frame
	void Update () {
		UpdateKeyFrames();
		GenerateFingerPoints();
		if(Input.GetKeyDown(KeyCode.R)) {
			StartCoroutine(MoveFinger());
		}
	}

	void UpdateKeyFrames() {
		fingerAnimationCurve.keys = new Keyframe[0];
		fingerAnimationCurve.AddKey(new Keyframe(0, 0));
		Vector2[] colliderPoints = new Vector2[transform.childCount];
		for(int i = 1; i < transform.childCount + 1; i++) {
			fingerAnimationCurve.AddKey(new Keyframe(i, transform.GetChild(i - 1).localPosition.y));
			colliderPoints[i - 1] = new Vector2(transform.GetChild(i - 1).localPosition.x, transform.GetChild(i - 1).localPosition.y);
		}
		collider.points = colliderPoints;
	}

	void GenerateFingerPoints() {
		
		float i = 0;
		//while(i < fingerDensity) {
		Vector3[] positions = new Vector3[finger.numPositions];
		finger.GetPositions(positions);
		for(int k = 0; k < finger.numPositions; k++){	
			finger.SetPosition(k, new Vector3(i, fingerAnimationCurve.Evaluate(i)));
			i += Time.fixedDeltaTime;
		}
	}

	IEnumerator MoveFinger() {
		float travelTime = 2;
		float t = 0;
		finger.numPositions = 0;
		AnimationCurve fingerCurve = new AnimationCurve();
		fingerCurve.AddKey(new Keyframe(0, 0));
		fingerCurve.AddKey(new Keyframe(travelTime * 0.7f, 1));
		fingerCurve.AddKey(new Keyframe(travelTime, 0.7f));

		while(t < travelTime) {
			finger.numPositions = finger.numPositions + 1;
			finger.SetPosition(finger.numPositions - 1, new Vector3(t * 3, fingerCurve.Evaluate(t)));
			t += Time.deltaTime;
			yield return null;
		}
	}
}
