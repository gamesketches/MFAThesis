using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AsokobanPlayer : MonoBehaviour {

	public float speed = 1;
	Animator animator;

	System.Collections.Generic.List<Direction> currentPath;
	Direction[] correctPath;

	SpriteRenderer spriteRenderer;


	public GameObject rooms;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>(); 
		correctPath = new Direction[4] {Direction.Up, Direction.Right, Direction.Right, Direction.Left};
		currentPath = new System.Collections.Generic.List<Direction>();

		spriteRenderer = GetComponent<SpriteRenderer>();

	}
	
	// Update is called once per frame
	void Update () {
		int hori = Mathf.RoundToInt(Input.GetAxis("Horizontal"));
		int vert = Mathf.RoundToInt(Input.GetAxis("Vertical"));
		if(vert > 0) {
			animator.SetInteger("direction", (int)Direction.Up);
		}
		else if(vert < 0) {
			animator.SetInteger("direction", (int)Direction.Down);
		}
		else if(hori < 0) {
			animator.SetInteger("direction", (int)Direction.Left);
			spriteRenderer.flipX = false;
		}
		else if(hori > 0) {
			animator.SetInteger("direction", (int)Direction.Right);
			spriteRenderer.flipX = true;
		}
		else {
			animator.SetInteger("direction", -1);
		}
		transform.Translate(hori * speed * Time.deltaTime, vert * speed * Time.deltaTime, 0);
	}

}
