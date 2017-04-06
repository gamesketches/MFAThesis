using UnityEngine;
using System.Collections;
using System;
using System.Reflection;
using UnityEngine.UI;

public class MenuActionScript : MonoBehaviour {

	private MethodInfo action;
	private object actionObject;
	private object[] parameters;
	public bool listOption;
	private Transform list;
	private Image[] images;
	private Text[] text;
	// Use this for initialization
	void Awake () {
		listOption = false;
		parameters = new object[0];
	}
	
	public void Initialize(MethodInfo method, object obj) {
		action = method;
		actionObject = obj;
	}

	public void Initialize(MethodInfo method, object obj, object[] methodParameters) {
		action = method;
		actionObject = obj;
		parameters = methodParameters;
	}

	public void InitializeAsList(Transform newList) {
		listOption = true;
		list = newList;
		list.gameObject.SetActive(false);
	}

	public void Activate() {
		if(listOption) {
			list.gameObject.SetActive(true);
			//ChangeOpacityImages((float)183 / (float)255);
			//ChangeOpacityText(1);
		}
		else {
			action.Invoke(actionObject, parameters);
		}
	}

	public void DeActivate() {
		list.gameObject.SetActive(false);
		//ChangeOpacityImages(0.2f);
		//ChangeOpacityText(0.5f);
	}

	void ChangeOpacityImages(float targetAlpha) {
		if(images == null) {
			images = list.gameObject.GetComponentsInChildren<Image>();
		}
		for(int i = 0; i < images.Length; i++) {
			Color oldColor = images[i].color;
			oldColor.a = targetAlpha;
			images[i].color = oldColor;
		}
	}

	void ChangeOpacityText(float targetAlpha){
		if(text == null) {
			text = list.gameObject.GetComponentsInChildren<Text>();
		}
		for(int i = 0; i < text.Length; i++) {
			Color oldColor = text[i].color;
			oldColor.a = targetAlpha;
			text[i].color = oldColor;
		}
	}
}
