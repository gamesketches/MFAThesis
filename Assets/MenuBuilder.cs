using UnityEngine;
using UnityEngine.UI;
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

		Debug.Log(menuXML.ChildNodes[1].FirstChild);
		XmlNode topNode = menuXML.ChildNodes[1].FirstChild;

		GenMenuList(topNode, transform);

		MethodInfo method = playerType.GetMethod(topNode.FirstChild.InnerText);
		method.Invoke(player, new object[0]);
	}
	
	// Update is called once per frame
	void GenMenuList (XmlNode topNode, Transform parent) {
		GameObject listOption = (GameObject)Instantiate(Resources.Load<GameObject>("prefabs/OptionList"));
		listOption.transform.parent = transform;
		foreach(XmlNode node in topNode.ChildNodes) {
			if(node.Name == "Action") {
				GameObject actionOption = (GameObject)Instantiate(Resources.Load<GameObject>("prefabs/MenuAction"));
				actionOption.transform.parent = listOption.transform;
				actionOption.GetComponentInChildren<Text>().text = node.InnerText;
			}
			else if(node.Name == "List") {
				GenMenuList(node, listOption.transform);
			}
			else Debug.Log(node.InnerText);
		}
	}
}
