using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

	Transform currentOption;
	Transform[] currentOptionList;
	int listPosition;
	GameObject pointer;
	// Use this for initialization
	void Awake () {
		pointer = (GameObject)Instantiate(Resources.Load<GameObject>("prefabs/MenuSelector"));
		Debug.Log(pointer);
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
			if(siblingIndex >= currentOption.transform.parent.childCount) {
				siblingIndex = 0;
			}
			currentOption = currentOption.transform.parent.GetChild(siblingIndex);
		}

		if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) {
			int siblingIndex = currentOption.GetSiblingIndex() - 1;
			if(siblingIndex < 0) {
				siblingIndex = currentOption.transform.parent.childCount - 1;
			}
			currentOption = currentOption.transform.parent.GetChild(siblingIndex);
		}
		if(Input.GetKeyDown(KeyCode.Space)) {
			MenuActionScript actionScript = currentOption.GetComponent<MenuActionScript>();
			actionScript.Activate();
			if(actionScript.listOption) {
				currentOptionList = currentOption.transform.GetChild(1).GetComponentsInChildren<Transform>();
				listPosition = 1;
				currentOption = currentOptionList[listPosition];
			}
		}
		UpdatePosition();
	}

	void UpdatePosition() {
		pointer.transform.position = currentOption.position - new Vector3(230, 0, 0);
	}
}
