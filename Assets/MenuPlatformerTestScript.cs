using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using System;

public class MenuPlatformerTestScript : MonoBehaviour {

	public Vector3 cursorStartPos;

	// Use this for initialization
	void Start () {
		MenuPlatformerCursorTest pointer = GetComponent<MenuPlatformerCursorTest>();

		MenuBasedPlatformerMovement player = GameObject.FindGameObjectWithTag("Player").GetComponent<MenuBasedPlatformerMovement>();
		Type menuType = Type.GetType("MenuBasedPlatformerMovement");
		pointer.Initialize(transform.GetChild(0));
		foreach(MenuActionScript actionScript in GetComponentsInChildren<MenuActionScript>(true)) {
			actionScript.Initialize(menuType.GetMethod(actionScript.transform.GetChild(0).GetComponent<Text>().text), player);
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
