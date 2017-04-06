using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerBehavior : MonoBehaviour {

	LineRenderer lineRenderer;
	public AnimationCurve fingerAnimationCurve;
	EdgeCollider2D collider;
	public Vector3 targetPosition;
	public float fingerResolution;
	public bool myTurn;
	// Use this for initialization
	void Start () {
		lineRenderer = GetComponent<LineRenderer>();
		fingerAnimationCurve = new AnimationCurve();
		collider = GetComponent<EdgeCollider2D>();
		InitializeFinger(new Vector3(0.5f, -3));
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.anyKeyDown) {
			//targetPosition += Random.value * new Vector3(2, 2, 0);
			if(myTurn) UpdateFinger();
			myTurn = !myTurn;
		}
		UpdateKeyFrames();
		UpdateFingerPoints();
	}

	public void InitializeFinger(Vector3 target) {
		targetPosition = target;
		float angleRad = Mathf.Atan2(target.y - transform.position.y, target.x - transform.position.x);
		transform.rotation = Quaternion.Euler(0, 0, angleRad * Mathf.Rad2Deg);

		for(int i = 0; i < transform.childCount; i++) {
			transform.GetChild(i).localPosition = new Vector3((Vector3.Distance(Vector3.zero, transform.worldToLocalMatrix.MultiplyPoint(target)) / transform.childCount) * i, Random.value * 2);
		}
		//transform.GetChild(transform.childCount - 1).position = transform.worldToLocalMatrix.MultiplyPoint(target);

		fingerAnimationCurve.AddKey(new Keyframe(0, 0));
		Vector2[] colliderPoints = new Vector2[transform.childCount];
		for(int i = 1; i < transform.childCount; i++) {
			fingerAnimationCurve.AddKey(new Keyframe(transform.GetChild(i - 1).localPosition.x , transform.GetChild(i - 1).localPosition.y));
			colliderPoints[i - 1] = new Vector2(transform.GetChild(i - 1).localPosition.x, transform.GetChild(i - 1).localPosition.y);
		}
		collider.points = colliderPoints;
		StartCoroutine(AnimateFingerInit());
	}

	void UpdateFinger() {
		Rigidbody2D tip = transform.GetChild(transform.childCount - 1).GetComponent<Rigidbody2D>();
		tip.MovePosition(new Vector2(Random.Range(-3f, 3f), Random.Range(-2, -4)));
		//tip.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
	}

	void UpdateKeyFrames() {
		fingerAnimationCurve.keys = new Keyframe[0];
		fingerAnimationCurve.AddKey(new Keyframe(0, 0));
		Vector2[] colliderPoints = new Vector2[transform.childCount];
		for(int i = 1; i < transform.childCount + 1; i++) {
			fingerAnimationCurve.AddKey(new Keyframe(transform.GetChild(i - 1).localPosition.x, transform.GetChild(i - 1).localPosition.y));
			colliderPoints[i - 1] = new Vector2(transform.GetChild(i - 1).localPosition.x, transform.GetChild(i - 1).localPosition.y);
		}
		collider.points = colliderPoints;
	}

	void UpdateFingerPoints() {
		
		float i = 0;
		//while(i < fingerDensity) {
		Vector3[] positions = new Vector3[lineRenderer.numPositions];
		lineRenderer.GetPositions(positions);
		float distancePerPoint = (float)fingerAnimationCurve.keys[fingerAnimationCurve.keys.Length - 1].time / (float)lineRenderer.numPositions;
		for(int k = 0; k < lineRenderer.numPositions; k++){	
			lineRenderer.SetPosition(k, new Vector3(distancePerPoint * k, fingerAnimationCurve.Evaluate(distancePerPoint * k)));
		}
	}

	IEnumerator AnimateFingerInit() {
		float t = 0;
		while(t < Vector3.Distance(Vector3.zero, transform.worldToLocalMatrix.MultiplyPoint(targetPosition))) {
			lineRenderer.numPositions = lineRenderer.numPositions + 1;
			lineRenderer.SetPosition(lineRenderer.numPositions -1, new Vector3(t, fingerAnimationCurve.Evaluate(t)));
			t += Time.deltaTime;
			yield return null;
		}
	}
}
