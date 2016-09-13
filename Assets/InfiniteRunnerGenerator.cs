using UnityEngine;
using System.Collections;

public class InfiniteRunnerGenerator : MonoBehaviour {

	public GameObject groundPrefab;
	public int BPM;
	public int generationStartPoint;
	private float timeForBeat;
	private int framesPerBeat = 15;
	// Use this for initialization
	void Start () {
		timeForBeat = (60000 / (float)BPM) / 1000; // 1 minute (60,000 ms) / BPM / 1000 to convert to seconds
		Debug.Log(timeForBeat);
		GenBlocks();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		foreach(GameObject obj in GameObject.FindGameObjectsWithTag("ground")) {
			obj.transform.Translate(new Vector3(/*-framesPerBeat * Time.fixedDeltaTime*/-1 / (float)framesPerBeat, 0f, 0f));
			if(obj.transform.position.x < -100) {
				Destroy(obj);
			}
		}
	}

	void GenBlocks() {
		int beatMarker = 0;
		float xOffset = 0;
		while(beatMarker < 4) {
			int tempBeats = Random.Range(0, 3);
			Vector3 placement = transform.position;
			placement.x += xOffset;
			GameObject newBrick = (GameObject)Instantiate(groundPrefab, placement, Quaternion.identity);
			Vector3 currentScale = newBrick.transform.lossyScale;
			currentScale.x = timeForBeat * tempBeats;
			newBrick.transform.localScale = currentScale;
			if(Random.value > 0.5) {
				tempBeats += 1;
			}
			beatMarker += tempBeats;
			xOffset += tempBeats * timeForBeat;
		}
		Debug.Log(timeForBeat * 3);
		Invoke("GenBlocks", timeForBeat * 3);
	}
}
