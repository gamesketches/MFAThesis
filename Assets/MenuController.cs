using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

	Transform currentOption;
	Transform[] currentOptionList;
	int listPosition;
	GameObject pointer;
	// Use this for initialization
	void Start () {
		pointer = (GameObject)Instantiate(Resources.Load<GameObject>("prefabs/MenuSelector"));
		pointer.transform.SetParent(GameObject.Find("Canvas").transform);
		listPosition = 1;
	}

	public void Initialize(Transform optionList) {
		currentOptionList = optionList.GetComponentsInChildren<Transform>();
		currentOption = currentOptionList[listPosition];
		pointer.transform.position = currentOption.position - new Vector3(230, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) {
			int siblingIndex = currentOption.GetSiblingIndex() + 1;
			if(siblingIndex > currentOption.transform.childCount) {
				siblingIndex = 0;
			}
			currentOption = currentOption.transform.parent.GetChild(siblingIndex);
		}

		if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) {
			int siblingIndex = currentOption.GetSiblingIndex() - 1;
			if(siblingIndex < 0) {
				siblingIndex = currentOption.transform.childCount - 1;
			}
			currentOption = currentOption.transform.parent.GetChild(siblingIndex);
		}
		if(Input.GetKeyDown(KeyCode.Space)) {
			Debug.Log(currentOption.name);
			currentOption.GetComponent<MenuActionScript>().Activate();
		}
		UpdatePosition();
	}

	void UpdatePosition() {
		pointer.transform.position = currentOption.position - new Vector3(230, 0, 0);
	}
}
