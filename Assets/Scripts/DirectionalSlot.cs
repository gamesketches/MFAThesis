using UnityEngine;
using System.Collections;

public class DirectionalSlot : MonoBehaviour {

	public bool occupied;
	public Direction direction;
	// Use this for initialization
	void Start () {
		occupied = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "directionBlock") {
			//if(other.gameObject.GetComponent<DirectionalBlockBehavior>().direction == direction) {
			occupied = true;
			GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("FullAnemone");
			Destroy(other.gameObject);
			Vector3 bubblePosition = transform.position;
			bubblePosition.z = -2;
			Instantiate(Resources.Load<GameObject>("prefabs/BubbleParticles"), bubblePosition, Quaternion.identity);
			//}
		}
	}
}
