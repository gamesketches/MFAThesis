using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class MenuController : MonoBehaviour {

	Transform currentOption;
	Transform[] currentOptionList;
	int listPosition;
	GameObject pointer;
	// Use this for initialization
	void Awake () {
		pointer = (GameObject)Instantiate(Resources.Load<GameObject>("prefabs/MenuSelector"));
		listPosition = 1;
	}

	public void Initialize(Transform optionList) {
		currentOptionList = optionList.GetComponentsInChildren<Transform>();
		currentOption = currentOptionList[listPosition];
		pointer.transform.position = currentOption.position - new Vector3(230, 0, 0);
		pointer.transform.SetParent(GameObject.Find("Canvas").transform);
		AddNode(new string[] {"Move", "Walk"});
	}

	public void ClearOptionList() {
		foreach(Transform option in currentOptionList) {
			Destroy(option.gameObject);
		}
	}

	void AddNode(string[] optionTree) {
		Transform targetList = currentOption.root;
		Text[] texts = currentOption.root.GetComponentsInChildren<Text>();
		for(int i = 0; i < optionTree.Length; i++) {
			foreach(Text option in texts) {
				if(option.text == optionTree[i]){
					targetList = option.transform.parent.GetChild(1);
					targetList.parent.GetComponent<MenuActionScript>().Activate();
					texts = targetList.GetComponentsInChildren<Text>();
					targetList.parent.GetComponent<MenuActionScript>().DeActivate();
					break;
				}
			}
		}
		MenuBasedPlatformerMovement player = GameObject.FindGameObjectWithTag("Player").GetComponent<MenuBasedPlatformerMovement>();
		Type playerType = Type.GetType("MenuBasedPlatformerMovement");
		GameObject actionOption = (GameObject)Instantiate(Resources.Load<GameObject>("prefabs/MenuAction"), targetList.GetChild(targetList.childCount - 1).position, Quaternion.identity);
		actionOption.transform.SetParent(targetList, false);
		actionOption.GetComponent<MenuActionScript>().Initialize(playerType.GetMethod("PrintTest"), player);
		actionOption.GetComponentInChildren<Text>().text = "hi";
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
		if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
			MenuActionScript actionScript = currentOption.GetComponent<MenuActionScript>();
			actionScript.Activate();
			if(actionScript.listOption) {
				currentOptionList = currentOption.transform.GetChild(1).GetComponentsInChildren<Transform>();
				listPosition = 1;
				currentOption = currentOptionList[listPosition];
			}
		}
		if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
			Transform grandParent = currentOption.transform.parent.parent;
			if(grandParent != transform.root) {
				currentOptionList = grandParent.parent.GetComponentsInChildren<Transform>();
				for(int i = 0; i < currentOptionList.Length; i++) {
					if(currentOptionList[i] == grandParent) {
						listPosition = i;
						break;
					}
				}
				currentOption = grandParent;
				grandParent.GetComponent<MenuActionScript>().DeActivate();
			}
		}
		UpdatePosition();
	}

	void UpdatePosition() {
		pointer.transform.position = currentOption.position - new Vector3(230, 0, 0);
	}
}
