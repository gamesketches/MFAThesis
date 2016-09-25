using UnityEngine;
using System.Collections;

public class CameraTargetManager : MonoBehaviour {

	public float switchFocusX;
	UnityStandardAssets._2D.Camera2DFollow followScript;
	public Transform cube;

	// Use this for initialization
	void Start () {
		followScript = Camera.main.GetComponent<UnityStandardAssets._2D.Camera2DFollow>();
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.x >= switchFocusX) {
			StartCoroutine(EyesOnMe(cube));
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
