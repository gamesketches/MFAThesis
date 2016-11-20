using UnityEngine;
using System.Collections;

public class CreateLevel : MonoBehaviour {

	public int cubeSlots;
	// Use this for initialization
	void Start () {
		GameObject CubeyBoy = Resources.Load<GameObject>("prefabs/Cube");
		for(int i = 0; i < cubeSlots; i++) {
			Instantiate(CubeyBoy, new Vector3(Random.Range(3, 400), 0.17f, 0), Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
