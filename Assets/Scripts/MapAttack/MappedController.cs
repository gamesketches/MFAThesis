using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public enum Direction {Up, Down, Left, Right};

public class MappedController : MonoBehaviour {

	public float speed = 1;

	System.Collections.Generic.Dictionary<Direction, KeyCode> controls;
	System.Collections.Generic.List<Direction> currentPath;
	Direction[] correctPath;

	SpriteRenderer miniMap;
	SpriteRenderer spriteRenderer;


	public GameObject rooms;

	// Use this for initialization
	void Start () {
		correctPath = new Direction[4] {Direction.Up, Direction.Right, Direction.Right, Direction.Left};
		currentPath = new System.Collections.Generic.List<Direction>();

		miniMap = GameObject.Find("map").GetComponent<SpriteRenderer>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		controls = new System.Collections.Generic.Dictionary<Direction, KeyCode>();
		controls.Add(Direction.Up, KeyCode.W);
		controls.Add(Direction.Down, KeyCode.S);
		controls.Add(Direction.Left, KeyCode.A);
		controls.Add(Direction.Right, KeyCode.D);
		foreach(Transform obj in rooms.transform) {
			obj.gameObject.SetActive(false);
		}

		rooms.transform.GetChild(0).gameObject.SetActive(true);
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
		rooms.transform.GetChild(currentPath.Count).gameObject.SetActive(false);
		KeyCode curUp = controls[Direction.Up];
		KeyCode curDown = controls[Direction.Down];
		KeyCode curLeft = controls[Direction.Left];
		KeyCode curRight = controls[Direction.Right];
		switch(entered) {
			case Direction.Up:
				Debug.Log("no change");
				break;
			case Direction.Down:
				controls[Direction.Up] = curDown;
				controls[Direction.Down] = curUp;
				controls[Direction.Left] = curRight;
				controls[Direction.Right] = curLeft;
				break;
			case Direction.Right:
				controls[Direction.Up] = curRight;//KeyCode.D;
				controls[Direction.Down] = curLeft;//KeyCode.A;
				controls[Direction.Left] = curUp;//KeyCode.W;
				controls[Direction.Right] = curDown;//KeyCode.S;
				break;
			case Direction.Left:
				controls[Direction.Up] = curLeft;//KeyCode.A;
				controls[Direction.Down] = curRight;//KeyCode.D;
				controls[Direction.Left] = curDown;//KeyCode.S;
				controls[Direction.Right] = curUp;//KeyCode.W;
				break;
			}
		currentPath.Add(entered);
		for(int i = 0; i < currentPath.Count; i++) {
			if(correctPath[i] != currentPath[i]) {
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
				currentPath.Clear();
				break;
				}
			}
	    rooms.transform.GetChild(currentPath.Count).gameObject.SetActive(true);
		miniMap.sprite = Resources.LoadAll<Sprite>("minimaps")[currentPath.Count];
		}
}
