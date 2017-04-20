using UnityEngine;
using System.Collections;
using System;
using System.Reflection;
using UnityEngine.UI;

public class MenuActionCatalogList : MonoBehaviour {

	private MethodInfo action;
	private object actionObject;
	private object[] parameters;
	public bool listOption;
	private Transform list;
	private Image[] images;
	private Text[] text;
	// Use this for initialization
	void Awake () {
		listOption = true;
		list = transform.GetChild(2);
		//list.gameObject.SetActive(false);
	}
	
	public void Initialize(MethodInfo method, object obj) {
		action = method;
		actionObject = obj;
		Debug.Log("Initialized with " + actionObject);
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
			StartCoroutine(SlideOut());
			//ChangeOpacityImages((float)183 / (float)255);
			//ChangeOpacityText(1);
		}
		else {
			action.Invoke(actionObject, parameters);
		}
	}

	public void DeActivate() {
		//list.gameObject.SetActive(false);
		StartCoroutine(SlideIn());
		//ChangeOpacityImages(0.2f);
		//ChangeOpacityText(0.5f);
	}

	IEnumerator SlideOut() {
		float movementTime = 0.3f;
		Vector3 startPos = transform.position;
		Vector3 endPos = transform.position + new Vector3(0, -100, 0);
		for(float t = 0; t <= movementTime; t+= Time.deltaTime) {
			transform.position = Vector3.Slerp(startPos, endPos, t / movementTime);
			yield return null;
		}
	}

	IEnumerator SlideIn() {
		float movementTime = 0.3f;
		Vector3 startPos = transform.position;
		Vector3 endPos = transform.position + new Vector3(0, 100, 0);
		for(float t = 0; t <= movementTime; t+= Time.deltaTime) {
			transform.position = Vector3.Slerp(startPos, endPos, t / movementTime);
			yield return null;
		}
		list.gameObject.SetActive(false);
	}
}
