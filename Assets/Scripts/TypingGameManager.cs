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

		public Phrase(string content, KeyCode left, KeyCode right, float time) {
			textContent = content;
			leftHeldKey = left;
			rightHeldKey = right;
			timeLimit = time;
		}


	}

	Queue<Phrase> phrases;
	public Text timer;
	public Text currentText;
	public Text leftHoldText;
	public Text rightHoldText;
	Phrase currentPhrase;
	int currentPhraseIndex;
	float currentTime;

	// Use this for initialization
	void Start () {
		phrases = new Queue<Phrase>();
		phrases.Enqueue(new Phrase("I like Chicken", KeyCode.G, KeyCode.J, 20));
		phrases.Enqueue(new Phrase("I like Liver", KeyCode.T, KeyCode.Y, 20));
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
		if(currentPhraseIndex == currentPhrase.textContent.Length) {
					Debug.Log("Nice!");
					SwitchPhrase();
					return;
		}
		if(currentPhrase.textContent[currentPhraseIndex] == ' '){
			currentPhraseIndex += 1;
		}
		Debug.Log(currentPhraseIndex);
		currentText.text = string.Concat("<color=black>", currentPhrase.textContent.Substring(0, currentPhraseIndex), "</color>", currentPhrase.textContent.Substring(currentPhraseIndex));
	}

	bool KeysStillHeld() {
		Color heldKeyColor = new Color(95f / 255f , 103f / 255f, 253f / 255f);
		leftHoldText.color = Input.GetKey(currentPhrase.leftHeldKey) ? Color.yellow : heldKeyColor;
		rightHoldText.color = Input.GetKey(currentPhrase.rightHeldKey) ? Color.yellow : heldKeyColor;
		if(Input.anyKey && Input.GetKey(currentPhrase.leftHeldKey) && Input.GetKey(currentPhrase.rightHeldKey)) {
			return true;
		}
		else {
			currentPhraseIndex = 0;
			return false;
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
			}
		else {
			currentText.text = "YOU WIN";
		}
	}
}
