using UnityEngine;
using System.Collections;

public class ShmupControls : MonoBehaviour {

	public float speed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float hori = Input.GetAxis("Horizontal");
		float vert = Input.GetAxis("Vertical");
		transform.Translate(hori * speed * Time.deltaTime, vert * speed * Time.deltaTime, 0);

		if(Input.GetKeyDown(KeyCode.Space)) {
			Instantiate(Resources.Load<GameObject>("prefabs/ShmupProjectile"), transform.position, Quaternion.identity);
		}
	}
}
