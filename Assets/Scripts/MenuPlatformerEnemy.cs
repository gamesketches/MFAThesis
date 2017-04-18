using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuPlatformerEnemy : MonoBehaviour {

	public enum EnemyType {Ground, Mid, Air};

	public EnemyType myType;

	delegate void UpdateFunction();

	public float movementRange;
	public float airSpeed;

	UpdateFunction myUpdate;

	Vector3 startPos;
	// Use this for initialization
	void Start () {
		switch(myType) {
		case EnemyType.Air:
			GetComponent<Rigidbody2D>().gravityScale = 0;
			myUpdate = AirUpdate;
			break;
		case EnemyType.Mid:
			myUpdate = MidUpdate;
			break;
		default:
			myUpdate = GroundUpdate;
			break;
		}
		startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		myUpdate();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Player") {
			if(!other.gameObject.GetComponent<MenuBasedPlatformerMovement>().blocking) {
				SceneManager.LoadScene(0);	
			}
		}
		else {
			Debug.Log(other.gameObject.name);
		}
	}

	void GroundUpdate() {
		transform.Translate(Mathf.PingPong(Time.time, movementRange) - movementRange / 2, 0, 0);
	}

	void MidUpdate() {

	}

	void AirUpdate() {
		transform.Translate((Mathf.Sin(Time.time) * movementRange), Mathf.Sin(Time.time * airSpeed) * movementRange, 0);
	}
}
