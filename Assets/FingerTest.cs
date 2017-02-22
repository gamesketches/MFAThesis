using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerTest : MonoBehaviour {

	LineRenderer finger;
	// Use this for initialization
	void Start () {
		finger = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
		finger.numPositions = transform.childCount + 1;
		finger.SetPosition(0, transform.position);
		for(int i = 0; i < transform.childCount; i++) {
			finger.SetPosition(i + 1, transform.GetChild(i).position);
		}
	}
}
