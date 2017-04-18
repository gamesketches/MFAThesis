﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System.Xml;
using System;
using System.Reflection;

public enum playerClass {Fencer, Acrobat, Swashbuckler};

public class MenuBuilder : MonoBehaviour {

	XmlDocument menuXML;
	MenuBasedPlatformerMovement player;
	Type playerType;
	MenuController pointer;
	public Vector3 startPosition;
	public playerClass myClass;
	public Vector3 listEntryOffset;
	public Vector3 subListOffset;

	// Use this for initialization
	void Start () {
		RectTransform rect = Resources.Load<GameObject>("prefabs/MenuAction").GetComponent<RectTransform>();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<MenuBasedPlatformerMovement>();
		TextAsset menuData = Resources.Load("MenuXML" + myClass.ToString()) as TextAsset;

		playerType = Type.GetType("MenuBasedPlatformerMovement");
		menuXML = new XmlDocument();
		menuXML.LoadXml(menuData.text);

		XmlNode topNode = menuXML.ChildNodes[1].FirstChild;

		pointer = GetComponent<MenuController>();


		pointer.Initialize(GenMenuList(topNode, transform, startPosition));

	}
	
	// Update is called once per frame
	Transform GenMenuList (XmlNode topNode, Transform parent, Vector3 position) {
		Vector3 newPos = position;
		GameObject listOption = (GameObject)Instantiate(Resources.Load<GameObject>("prefabs/OptionList"), newPos, Quaternion.identity);
		listOption.transform.SetParent(parent, false);
		int numOptionsInList = 0;
		foreach(XmlNode node in topNode.ChildNodes) {
			if(node.Name == "Action") {
				GameObject actionOption = (GameObject)Instantiate(Resources.Load<GameObject>("prefabs/MenuAction"), newPos, Quaternion.identity);
				actionOption.transform.SetParent(listOption.transform, false);
				actionOption.GetComponent<MenuActionScript>().Initialize(playerType.GetMethod(node.InnerText), player);
				actionOption.GetComponentInChildren<Text>().text = node.InnerText;
			}
			else if(node.Name == "List") {
				GameObject actionOption = (GameObject)Instantiate(Resources.Load<GameObject>("prefabs/MenuAction"), newPos, Quaternion.identity);
				actionOption.transform.SetParent(listOption.transform, false);
				actionOption.GetComponent<MenuActionScript>().InitializeAsList(GenMenuList(node, actionOption.transform, subListOffset));//childPos));
				actionOption.GetComponentInChildren<Text>().text = node.Attributes[0].Value;
			}
			else Debug.Log(node.InnerText);

			numOptionsInList++;
			newPos += listEntryOffset;
		}

		return listOption.transform;
	}
}
