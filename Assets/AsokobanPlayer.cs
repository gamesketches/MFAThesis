using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AsokobanPlayer : MonoBehaviour {

	public float speed = 1;

	System.Collections.Generic.List<Direction> currentPath;
	Direction[] correctPath;

	SpriteRenderer spriteRenderer;


	public GameObject rooms;

	// Use this for initialization
	void Start () {
		correctPath = new Direction[4] {Direction.Up, Direction.Right, Direction.Right, Direction.Left};
		currentPath = new System.Collections.Generic.List<Direction>();

		spriteRenderer = GetComponent<SpriteRenderer>();

	}
	
	// Update is called once per frame
	void Update () {
		int hori = Mathf.RoundToInt(Input.GetAxis("Horizontal"));
		int vert = Mathf.RoundToInt(Input.GetAxis("Vertical"));
		transform.Translate(hori * speed * Time.deltaTime, vert * speed * Time.deltaTime, 0);
		if(hori != 0) {
			if(hori > 0) {
				spriteRenderer.sprite = Resources.Load<Sprite>("scubaRight");
			}
			else {
				spriteRenderer.sprite = Resources.Load<Sprite>("scubaLeft");
			}
		}
		else {
			if(vert > 0) {
				spriteRenderer.sprite = Resources.Load<Sprite>("scubaUp");
			}
			else {
				spriteRenderer.sprite = Resources.Load<Sprite>("scubaDown");
			}
		}
	}

}
