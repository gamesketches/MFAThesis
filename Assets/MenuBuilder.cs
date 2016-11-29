using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System.Xml;
using System;
using System.Reflection;

public class MenuBuilder : MonoBehaviour {

	XmlDocument menuXML;
	MenuBasedPlatformerMovement player;
	Vector3 offset;
	Type playerType;

	// Use this for initialization
	void Start () {
		RectTransform rect = Resources.Load<GameObject>("prefabs/MenuAction").GetComponent<RectTransform>();
		offset = new Vector3(rect.rect.width, rect.rect.height, 0);
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<MenuBasedPlatformerMovement>();
		Debug.Log(player);
		TextAsset menuData = Resources.Load("MenuXML") as TextAsset;

		playerType = Type.GetType("MenuBasedPlatformerMovement");
		menuXML = new XmlDocument();
		menuXML.LoadXml(menuData.text);

		XmlNode topNode = menuXML.ChildNodes[1].FirstChild;

		GenMenuList(topNode, transform, new Vector3(0, 0, 0));

		//MethodInfo method = playerType.GetMethod(topNode.FirstChild.InnerText);
		//method.Invoke(player, new object[0]);
	}
	
	// Update is called once per frame
	void GenMenuList (XmlNode topNode, Transform parent, Vector3 position) {
		Vector3 newPos = position;
		newPos.x += offset.x;
		GameObject listOption = (GameObject)Instantiate(Resources.Load<GameObject>("prefabs/OptionList"), newPos, Quaternion.identity);
		listOption.transform.parent = transform;
		int numOptionsInList = 0;
		foreach(XmlNode node in topNode.ChildNodes) {
			if(node.Name == "Action") {
				GameObject actionOption = (GameObject)Instantiate(Resources.Load<GameObject>("prefabs/MenuAction"), newPos, Quaternion.identity);
				actionOption.transform.SetParent(parent, false);
				actionOption.GetComponent<MenuActionScript>().Initialize(playerType.GetMethod(node.InnerText), player);
				actionOption.GetComponentInChildren<Text>().text = node.InnerText;
			}
			else if(node.Name == "List") {
				GenMenuList(node, listOption.transform, newPos);
			}
			else Debug.Log(node.InnerText);

			numOptionsInList++;
			newPos.y -= offset.y;
		}
	}
}
