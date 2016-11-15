using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraTargetManager : MonoBehaviour {

	public float switchFocusX;
	UnityStandardAssets._2D.Camera2DFollow followScript;
	public Transform cube;
	private List<Transform> cubes;

	// Use this for initialization
	void Start () {
		cubes = new List<Transform>();
		foreach(GameObject qbe in GameObject.FindGameObjectsWithTag("red")) {
			cubes.Add(qbe.transform);
		}
		followScript = Camera.main.GetComponent<UnityStandardAssets._2D.Camera2DFollow>();
	}
	
	// Update is called once per frame
	void Update () {
		foreach(Transform qbe in cubes) {
			Debug.Log(Camera.main.orthographicSize);
			if(followScript.target.tag != "red" && transform.position.x >= qbe.position.x - Camera.main.orthographicSize) {
				StartCoroutine(EyesOnMe(qbe));
				cubes.RemoveAt(0);
				return;
			}
		}
		if(followScript.target == null) {
			StartCoroutine(EyesOnMe(GameObject.FindGameObjectWithTag("Player").transform));
		}
	}

	IEnumerator EyesOnMe(Transform target) {
		float t = 0;
		Vector3 startPos = Camera.main.transform.position;
		Vector3 targetPos = target.position;
		targetPos.z = startPos.z;
		while(t < 0) {
			Camera.main.transform.position = Vector3.Lerp(startPos, targetPos, t);
			t += Time.deltaTime;
			yield return null;
		}
		Debug.Log("done focusing");
		followScript.target = target;
	}
}
