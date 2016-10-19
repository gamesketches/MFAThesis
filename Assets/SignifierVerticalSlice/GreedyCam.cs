using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GreedyCam : MonoBehaviour {

	public float switchFocusX;
	UnityStandardAssets._2D.Camera2DFollow followScript;
	public Transform Gem;
	public float MinTimeToDistraction;
	public float MaxTimeToDistraction;
	private float timeToDistraction;
	private ParticleSystem dollaBills;

	// Use this for initialization
	void Start () {
		timeToDistraction = Random.Range(MinTimeToDistraction, MaxTimeToDistraction);
		followScript = Camera.main.GetComponent<UnityStandardAssets._2D.Camera2DFollow>();
		dollaBills = Camera.main.GetComponentInChildren<ParticleSystem>();
		dollaBills.Stop();
	}
	
	// Update is called once per frame
	void Update () {
		timeToDistraction -= Time.deltaTime;
		if(timeToDistraction <= 0) {
			StartCoroutine(EyesOnMe(Gem));
			dollaBills.Play();
			timeToDistraction = Random.Range(MinTimeToDistraction, MaxTimeToDistraction);
		}
		if(Input.GetKeyDown(KeyCode.E)) {
			StartCoroutine(EyesOnMe(GameObject.FindGameObjectWithTag("Player").transform));
			dollaBills.Stop();
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
		followScript.target = target;
	}
}
