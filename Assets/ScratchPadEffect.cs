using UnityEngine;
using System.Collections;

public class ScratchPadEffect : MonoBehaviour {

	public Shader shader;
	public Material mat;
	public int numLines = 1;
	Vector3 pos;
	Vector3 scratchArea;
	// Use this for initialization
	void Start () {

		mat = new Material(shader);
	}
	
	// Update is called once per frame
	void Update () {
		if(GameObject.Find("Juice(Clone)") != null) {
			GameObject juiceObj = GameObject.Find("Juice(Clone)");
			pos = juiceObj.transform.position;
			scratchArea = juiceObj.GetComponent<SpriteRenderer>().bounds.size;
		}
		else {
			pos = Vector3.zero;
		}
	}

	void OnPostRender() {
		float width = scratchArea.x / 2;
		float height = scratchArea.y / 2;


		//Vector3 pos = transform.position;
		GL.Color(Color.blue);
		GL.Begin(GL.LINES);
		mat.SetPass(0);
		for(int i = 0; i < numLines; i++) {
			Vector3 firstLinePoint = new Vector3(pos.x + (Random.Range(-width, width)), pos.y + Random.Range(-height, height), 0);
			Vector3 secondLinePoint = new Vector3(pos.x + (Random.Range(-width, width)), pos.y + Random.Range(-height, height), 0);
			GL.Vertex3(firstLinePoint.x, firstLinePoint.y, firstLinePoint.z);
			GL.Vertex3(secondLinePoint.x, secondLinePoint.y, secondLinePoint.z);
		}
		GL.End();
/*		mat.SetPass(0);
		GL.Begin(GL.LINES);
		GL.Color(new Color(1, 0, 0,1));

		GL.Vertex3(30, 0, 0);
		GL.Vertex3(40, 0, 0);
		GL.Vertex3(40, 10, 0);
		GL.Vertex3(30, 10, 0);
		GL.End();*/
	}
}
