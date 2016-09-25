using UnityEngine;
using System.Collections;

public class InfiniteRunnerGenerator : MonoBehaviour {

	GameObject groundPrefab;
	GameObject otherGroundPrefab;
	public int BPM;
	public int generationStartPoint;
	public float instantiateInterval;
	public float syncopationTime;
	private float timer;
	private float timeForBeat;
	public float startTimer;
	private int framesPerBeat = 15;
	private bool flipper;
	// Use this for initialization
	void Start () {
		groundPrefab = Resources.Load<GameObject>("prefabs/Ground");
		otherGroundPrefab = Resources.Load<GameObject>("prefabs/RedBrick");
		timer = instantiateInterval;
		flipper = true;
		timeForBeat = (60000 / (float)BPM) / 1000; // 1 minute (60,000 ms) / BPM / 1000 to convert to seconds
		Debug.Log(timeForBeat);
		// Actual rhythic block generator
		//GenBlocks();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		startTimer -= Time.fixedDeltaTime;
		if(startTimer < 0) {
			timer -= Time.fixedDeltaTime;
			if(timer <= 0) {
				GameObject temp;
				if(flipper) {
					temp = (GameObject)Instantiate(groundPrefab, transform.position, Quaternion.identity);
				}
				else {
					temp = (GameObject)Instantiate(otherGroundPrefab, transform.position, Quaternion.identity);
				}
				temp.transform.localScale = new Vector3(0.8f, 1f, 1f); 
				flipper = !flipper;
				timer = instantiateInterval;
			}
			foreach(GameObject obj in GameObject.FindGameObjectsWithTag("ground")) {
				obj.transform.Translate(new Vector3(-1 / (float)framesPerBeat, 0f, 0f));
				if(obj.transform.position.x < -100) {
					Destroy(obj);
				}
			}
			if(syncopationTime > 0) {
			syncopationTime -= Time.fixedDeltaTime;
			if(syncopationTime <= 0) {
				timer -= 0.215f;
			}
		}
		}
	}

	void NoFunZoneBlockGenerator() {
		
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
