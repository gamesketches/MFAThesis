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
	Vector3 offset;

	// Use this for initialization
	void Start () {
		RectTransform rect = Resources.Load<GameObject>("prefabs/MenuAction").GetComponent<RectTransform>();
		offset = new Vector3(rect.rect.width, rect.rect.height, 0);
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
		Vector3 newPos = parent.position;
		newPos.x += offset.x;
		GameObject listOption = (GameObject)Instantiate(Resources.Load<GameObject>("prefabs/OptionList"), newPos, Quaternion.identity);
		listOption.transform.parent = transform;
		int numOptionsInList = 0;
		foreach(XmlNode node in topNode.ChildNodes) {
			if(node.Name == "Action") {
				GameObject actionOption = (GameObject)Instantiate(Resources.Load<GameObject>("prefabs/MenuAction"), newPos, Quaternion.identity);
				actionOption.transform.parent = listOption.transform;
				actionOption.GetComponentInChildren<Text>().text = node.InnerText;
			}
			else if(node.Name == "List") {
				GenMenuList(node, listOption.transform);
			}
			else Debug.Log(node.InnerText);

			numOptionsInList++;
			newPos.y -= offset.y;
		}
	}
}
