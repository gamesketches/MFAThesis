using UnityEngine;
using System.Collections;
using System;
using System.Reflection;

public class MenuActionScript : MonoBehaviour {

	private MethodInfo action;
	private object actionObject;
	private object[] parameters;
	// Use this for initialization
	void Start () {
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

	public void Activate() {
		action.Invoke(actionObject, parameters);
	}
}
