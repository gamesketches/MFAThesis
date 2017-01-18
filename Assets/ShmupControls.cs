using UnityEngine;
using System.Collections;

public class ShmupControls : MonoBehaviour {

	public float speed;
	public float attackTime;
	private float attackTimer = 0;
	private ShmupEnemyBehavior caughtEnemy;
	private GameObject daJuice = null;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float hori = Input.GetAxis("Horizontal");
		float vert = Input.GetAxis("Vertical");
		transform.Translate(hori * speed * Time.deltaTime, vert * speed * Time.deltaTime, 0);

		if(Input.GetKeyDown(KeyCode.Space)) {
			if(caughtEnemy) {
				if(caughtEnemy.Squeeze() < 0.1) {
					Destroy(caughtEnemy.gameObject);
					Destroy(daJuice);
				}
				else {
					if(daJuice == null) {
						daJuice = (GameObject)Instantiate(Resources.Load<GameObject>("JuiceShmup/Juice"), transform.position, Quaternion.identity);
						daJuice.transform.SetParent(transform);
					}
					daJuice.transform.localScale += new Vector3(0, 2, 0);
					daJuice.transform.position += new Vector3(0, -0.5f, 0);
				}
			}
			else {
			//Instantiate(Resources.Load<GameObject>("prefabs/ShmupProjectile"), transform.position, Quaternion.identity);
				attackTimer = attackTime;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(attackTimer > 0 && other.tag == "ShmupEnemy") {
			other.transform.SetParent(transform);
			caughtEnemy = other.gameObject.GetComponent<ShmupEnemyBehavior>();
			caughtEnemy.caught = true;
		}
	}

	void OnTriggerStay2D(Collider2D other) {
		if(attackTimer > 0 && other.tag == "ShmupEnemy") {
			other.transform.SetParent(transform);
			caughtEnemy = other.gameObject.GetComponent<ShmupEnemyBehavior>();
			caughtEnemy.caught = true;
		}
	}
}
