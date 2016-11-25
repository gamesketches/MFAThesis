using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;
using System;
using System.Reflection;

public class MenuBuilder : MonoBehaviour {

	XmlDocument menuXML;
	AcrobaticPlatformer player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<AcrobaticPlatformer>();
		TextAsset menuData = Resources.Load("MenuXML") as TextAsset;

		Type playerType = Type.GetType("AcrobaticPlatformer");
		menuXML = new XmlDocument();
		menuXML.LoadXml(menuData.text);

		MethodInfo method = playerType.GetMethod(menuXML.ChildNodes[1].InnerText);
		method.Invoke(player, new object[0]);
	}
	
	// Update is called once per frame
	void Update () {
			
	}
}
