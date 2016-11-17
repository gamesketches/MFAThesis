using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TypingGameManager : MonoBehaviour {

	private class Phrase {
		public string textContent;
		public KeyCode leftHeldKey;
		public KeyCode rightHeldKey;
		public float timeLimit;
		public Vector3 position;

		public Phrase(string content, KeyCode left, KeyCode right, float time, Vector2 newPosition) {
			textContent = content;
			leftHeldKey = left;
			rightHeldKey = right;
			timeLimit = time;
			position = new Vector3(newPosition.x, newPosition.y, 0);
		}


	}

	Queue<Phrase> phrases;
	public Text timer;
	public Text currentText;
	public Text leftHoldText;
	public Text rightHoldText;
	public float offsetOnType;
	Phrase currentPhrase;
	int currentPhraseIndex;
	float currentTime;

	// Use this for initialization
	void Start () {
		Debug.Log(currentText.rectTransform.localPosition);
		phrases = new Queue<Phrase>();
		phrases.Enqueue(new Phrase("Type the letters", KeyCode.None, KeyCode.None, 30, Vector2.zero));
		phrases.Enqueue(new Phrase ("And Mind The Timer", KeyCode.None, KeyCode.None, 10, Vector2.zero));
		phrases.Enqueue(new Phrase("Blue Letters Must Be Held", KeyCode.V, KeyCode.K, 30, new Vector2(200, 0)));
		phrases.Enqueue(new Phrase("He goes to school", KeyCode.A, KeyCode.M, 20, new Vector2(300, 100)));
		phrases.Enqueue(new Phrase("Burgess in both videos", KeyCode.C, KeyCode.P, 20, new Vector2(100, -300)));
		SwitchPhrase();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateTimer();
		if(KeysStillHeld()) {
			if(Input.GetKeyDown(CurrentCharacter())) {
				UpdateTextData();
			}
		}
	}

	void UpdateTimer() {
		currentTime -= Time.deltaTime;
		if(currentTime < 0) {
			Debug.Log("GAME OVER");
		}
		else {
			timer.text = currentTime.ToString();
		}
	}

	void UpdateTextData() {
		currentPhraseIndex += 1;
		currentText.rectTransform.Translate(-offsetOnType, 0, 0);
		if(currentPhraseIndex == currentPhrase.textContent.Length) {
					Debug.Log("Nice!");
					SwitchPhrase();
					return;
		}
		if(currentPhrase.textContent[currentPhraseIndex] == ' '){
			currentPhraseIndex += 1;
		}
		currentText.text = string.Concat("<color=black>", currentPhrase.textContent.Substring(0, currentPhraseIndex), "</color>", currentPhrase.textContent.Substring(currentPhraseIndex));
	}

	bool KeysStillHeld() {
		if(currentPhrase.leftHeldKey == KeyCode.None && currentPhrase.rightHeldKey == KeyCode.None) {
			Color backgroundColor = new Color(247 / 255f, 248 / 255f, 233 / 255);
			leftHoldText.color = backgroundColor;
			rightHoldText.color = backgroundColor;
			return true;
		}
		Color heldKeyColor = new Color(95f / 255f , 103f / 255f, 253f / 255f);
		leftHoldText.color = Input.GetKey(currentPhrase.leftHeldKey) ? Color.yellow : heldKeyColor;
		rightHoldText.color = Input.GetKey(currentPhrase.rightHeldKey) ? Color.yellow : heldKeyColor;
		if(Input.anyKey && Input.GetKey(currentPhrase.leftHeldKey) && Input.GetKey(currentPhrase.rightHeldKey)) {
			return true;
		}
		else if (currentText.text != "YOU WIN STOP TYPING"){
			currentPhraseIndex = 0;
			currentText.text = currentPhrase.textContent;
			return false;
		}
		else {
			Color backgroundColor = new Color(247 / 255f, 248 / 255f, 233 / 255);
			leftHoldText.color = backgroundColor;
			rightHoldText.color = backgroundColor;
			return true;
		}
	}

	string CurrentCharacter() {
		return currentPhrase.textContent[currentPhraseIndex].ToString().ToLower();
	}

	void SwitchPhrase() {
		if(phrases.Count >= 1) {
			currentPhrase = phrases.Dequeue();
			currentText.text = currentPhrase.textContent;
			leftHoldText.text = currentPhrase.leftHeldKey.ToString();
			rightHoldText.text = currentPhrase.rightHeldKey.ToString();
			currentTime = currentPhrase.timeLimit;
			currentPhraseIndex = 0;
			currentText.rectTransform.localPosition = currentPhrase.position;
			}
		else {
			currentText.rectTransform.localPosition = Vector3.zero;
			currentText.text = "YOU WIN STOP TYPING";
		}
	}
}
