using UnityEngine;
using System.Collections;

public enum Direction {Up, Down, Left, Right};

public class MappedController : MonoBehaviour {

	public float speed = 1;

	System.Collections.Generic.Dictionary<Direction, KeyCode> controls;
	System.Collections.Generic.List<Direction> currentPath;
	Direction[] correctPath;

	SpriteRenderer miniMap;
	SpriteRenderer spriteRenderer;
	// Use this for initialization
	void Start () {
		correctPath = new Direction[4] {Direction.Up, Direction.Right, Direction.Right, Direction.Up};
		currentPath = new System.Collections.Generic.List<Direction>();

		miniMap = GameObject.Find("map").GetComponent<SpriteRenderer>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		controls = new System.Collections.Generic.Dictionary<Direction, KeyCode>();
		controls.Add(Direction.Up, KeyCode.W);
		controls.Add(Direction.Down, KeyCode.S);
		controls.Add(Direction.Left, KeyCode.A);
		controls.Add(Direction.Right, KeyCode.D);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(controls[Direction.Up])){
			transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
			spriteRenderer.sprite = Resources.Load<Sprite>("scubaUp");
		}
		else if(Input.GetKey(controls[Direction.Down])){
			transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
			spriteRenderer.sprite = Resources.Load<Sprite>("scubaDown");
		}

		if(Input.GetKey(controls[Direction.Right])) {
			transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
			spriteRenderer.sprite = Resources.Load<Sprite>("scubaRight");
		}
		else if(Input.GetKey(controls[Direction.Left])) {
			transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
			spriteRenderer.sprite = Resources.Load<Sprite>("scubaLeft");
		}
	}

	public void RemappControls(Direction entered) {
		switch(entered) {
			case Direction.Up:
				controls[Direction.Up] = KeyCode.W;
				controls[Direction.Down] = KeyCode.S;
				controls[Direction.Left] = KeyCode.A;
				controls[Direction.Right] = KeyCode.D;
				break;
			case Direction.Down:
				controls[Direction.Up] = KeyCode.S;
				controls[Direction.Down] = KeyCode.W;
				controls[Direction.Left] = KeyCode.D;
				controls[Direction.Right] = KeyCode.A;
				break;
			case Direction.Right:
				controls[Direction.Up] = KeyCode.D;
				controls[Direction.Down] = KeyCode.A;
				controls[Direction.Left] = KeyCode.W;
				controls[Direction.Right] = KeyCode.S;
				break;
			case Direction.Left:
				controls[Direction.Up] = KeyCode.A;
				controls[Direction.Down] = KeyCode.D;
				controls[Direction.Left] = KeyCode.S;
				controls[Direction.Right] = KeyCode.W;
				break;
			}
		currentPath.Add(entered);
		for(int i = 0; i < currentPath.Count; i++) {
			if(correctPath[i] != currentPath[i]) {
				currentPath.Clear();
				break;
				}
			}
		miniMap.sprite = Resources.LoadAll<Sprite>("minimaps")[currentPath.Count];
		}
}
