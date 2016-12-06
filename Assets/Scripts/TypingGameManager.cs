using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class TypingGameManager : MonoBehaviour {

	delegate void ManipulationFunction();

	private class Phrase {
		public string textContent;
		public KeyCode leftHeldKey;
		public KeyCode rightHeldKey;
		public float timeBonus;
		public Vector3 position;
		public Color myColor;
		public ManipulationFunction myFunc;

		public Phrase(string content, KeyCode left, KeyCode right, float time, Vector2 newPosition, Color newColor, ManipulationFunction newFunc) {
			textContent = content;
			leftHeldKey = left;
			rightHeldKey = right;
			timeBonus = time;
			position = new Vector3(newPosition.x, newPosition.y, 0);
			myColor = newColor;
			myFunc = newFunc;
		}
	}

	Queue<Phrase> phrases;
	public Text timer;
	public Text currentText;
	public Text leftHoldText;
	public Text rightHoldText;
	public Text score;
	public float offsetOnType;
	Color backgroundColor;
	Phrase currentPhrase;
	int currentPhraseIndex;
	float currentTime = 1;
	AudioSource audio;
	bool gameStarted;

	HighScoreManager highScoreList;

	// Use this for initialization
	void Start () {
		gameStarted = false;
		highScoreList = GetComponent<HighScoreManager>();
		backgroundColor = Camera.main.backgroundColor;
		phrases = new Queue<Phrase>();
		phrases.Enqueue(new Phrase("Type the letters", KeyCode.None, KeyCode.None, 14, Vector2.zero, backgroundColor, null));
		phrases.Enqueue(new Phrase ("And Mind The Timer", KeyCode.None, KeyCode.None, 5, Vector2.zero, backgroundColor, null));
		phrases.Enqueue(new Phrase("Blue Letters Must Be Held", KeyCode.None, KeyCode.K, 5, new Vector2(200, 0), backgroundColor, null));
		phrases.Enqueue(new Phrase("Letting go is starting over", KeyCode.None, KeyCode.J, 9, Vector2.zero, backgroundColor, null));
		phrases.Enqueue(new Phrase("Now it begins", KeyCode.R, KeyCode.U, 3, Vector2.zero, backgroundColor, null));
		phrases.Enqueue(new Phrase("The man is also filial piety", KeyCode.W, KeyCode.V,10, Vector3.zero, backgroundColor, null));
		phrases.Enqueue(new Phrase("And Good Guilty Of those who", KeyCode.R, KeyCode.Z, 5, Vector3.zero, backgroundColor, null));
		phrases.Enqueue(new Phrase("fresh bad guilty", KeyCode.X, KeyCode.P, 8, Vector3.zero, backgroundColor, null));
		phrases.Enqueue(new Phrase("and good for chaos", KeyCode.M, KeyCode.K, 7, Vector3.zero, backgroundColor, null));
		phrases.Enqueue(new Phrase("not the there", KeyCode.S, KeyCode.J, 3, Vector3.zero, backgroundColor, null));
		phrases.Enqueue(new Phrase("Gentleman of this", KeyCode.X, KeyCode.R, 5, Vector3.zero, backgroundColor, null));
		phrases.Enqueue(new Phrase ("the legislation and students", KeyCode.Q, KeyCode.V, 10, Vector2.zero, backgroundColor, null));
		currentPhrase = phrases.Dequeue();
		currentText.text = currentPhrase.textContent;
		leftHoldText.text = currentPhrase.leftHeldKey.ToString();
		rightHoldText.text = currentPhrase.rightHeldKey.ToString();
		currentTime += currentPhrase.timeBonus;
		currentPhraseIndex = 0;
		audio = GetComponent<AudioSource>();
		audio.clip = Resources.Load<AudioClip>("Sounds/TypingGame/type1");
		timer.text = currentTime.ToString("F");
	}
	
	// Update is called once per frame
	void Update () {
		if(!GameOver()) {
			if(gameStarted) {
				UpdateTimer();
			}
			if(currentTime > 0 && KeysStillHeld()) {
				if(currentPhraseIndex < currentPhrase.textContent.Length && Input.GetKeyDown(CurrentCharacter())) {
					UpdateTextData();
				}
			}
		}
	}

	void UpdateTimer() {
		currentTime -= Time.deltaTime;
		if(currentTime < 0) {
			currentText.rectTransform.localPosition = Vector3.zero;
			currentText.text = "OUT OF TIME";
			leftHoldText.color = Camera.main.backgroundColor;
			rightHoldText.color = Camera.main.backgroundColor;
			timer.text = 0.ToString();
			StartCoroutine(ResetGame());
		}
		else {
			timer.text = currentTime.ToString("F");
		}
	}

	void UpdateTextData() {
		gameStarted = true;
		currentPhraseIndex += 1;
		audio.Play();
		foreach(GameObject text in GameObject.FindGameObjectsWithTag("finishedText")){
			text.GetComponent<Text>().rectTransform.Translate(-offsetOnType, 0, 0);
		}
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
		Color heldKeyColor = new Color(95f / 255f , 103f / 255f, 253f / 255f);
		leftHoldText.color = Input.GetKey(currentPhrase.leftHeldKey) ? Color.yellow : heldKeyColor;
		rightHoldText.color = Input.GetKey(currentPhrase.rightHeldKey) ? Color.yellow : heldKeyColor;

		Color backgroundColor = new Color(247 / 255f, 248 / 255f, 233 / 255f);
		if(currentPhrase.leftHeldKey == KeyCode.None){
			leftHoldText.color = backgroundColor; 
		}
		if(currentPhrase.rightHeldKey == KeyCode.None) {
			rightHoldText.color = backgroundColor;
		}

		if(currentPhrase.leftHeldKey == currentPhrase.rightHeldKey) {
			return true;
		}

		if(Input.anyKey && KeyHeld(currentPhrase.leftHeldKey) && KeyHeld(currentPhrase.rightHeldKey)) {
			return true;
		}
		else if (currentText.text != "YOU WIN STOP TYPING" && currentText.text != "OUT OF TIME"){
			currentPhraseIndex = 0;
			currentText.text = currentPhrase.textContent;
			return false;
		}
		else {
			leftHoldText.color = backgroundColor;
			rightHoldText.color = backgroundColor;
			return true;
		}
	}

	bool KeyHeld(KeyCode inputKey) {
		if(inputKey == KeyCode.None || Input.GetKey(inputKey)) {
			return true;
		}
		else {
			return false;
		}
	}

	string CurrentCharacter() {
		return currentPhrase.textContent[currentPhraseIndex].ToString().ToLower();
	}

	void SwitchPhrase() {
		score.text = (int.Parse(score.text) + 1).ToString();
		if(phrases.Count >= 1) {
			StartCoroutine(WrapUpOldPhrase());
			}
		else {
			currentText.rectTransform.localPosition = Vector3.zero;
			currentText.text = "YOU WIN STOP TYPING";
			leftHoldText.color = Camera.main.backgroundColor;
			rightHoldText.color = Camera.main.backgroundColor;
			if(highScoreList.MadeHighScoreList((int)float.Parse(timer.text))){
				highScoreList.InputNewName((int)float.Parse(timer.text));
			}
			else {
				StartCoroutine(ResetGame());
			}
		}
	}

	IEnumerator WrapUpOldPhrase() {
		if(currentPhrase != null) {
			currentText.text = string.Concat("<color=black>", currentPhrase.textContent, "</color>");
	
			float t = 0;
			audio.clip = Resources.Load<AudioClip>("Sounds/TypingGame/slide");
			audio.Play();
			Vector3 offset = currentPhrase.position - currentText.rectTransform.localPosition;
			foreach(GameObject text in GameObject.FindGameObjectsWithTag("finishedText")) {
				StartCoroutine(MoveText(text.GetComponent<Text>(), offset));
			}
			yield return StartCoroutine(MoveText(currentText, offset));
			Text oldText = Instantiate(currentText, currentText.transform.parent) as Text;
			oldText.rectTransform.localPosition = currentPhrase.position;
			oldText.tag = "finishedText";
			oldText.color = Color.black;
		}
		audio.clip = Resources.Load<AudioClip>("Sounds/TypingGame/type1");
		currentPhrase = phrases.Dequeue();
		currentText.text = currentPhrase.textContent;
		leftHoldText.text = currentPhrase.leftHeldKey.ToString();
		rightHoldText.text = currentPhrase.rightHeldKey.ToString();
		currentTime += currentPhrase.timeBonus;
		currentPhraseIndex = 0;
		Camera.main.backgroundColor = currentPhrase.myColor;
		currentText.rectTransform.localPosition = currentPhrase.position + new Vector3(0, -200, 0);

		foreach(GameObject text in GameObject.FindGameObjectsWithTag("finishedText")) {
				StartCoroutine(MoveText(text.GetComponent<Text>(), new Vector3(0, 200, 0)));
			}
	}

	IEnumerator MoveText(Text theText, Vector3 offset) {
		float t = 0;
		Vector3 startPos = theText.rectTransform.localPosition;
		Vector3 endPos = startPos + offset;
		while(t < 1) {
			theText.rectTransform.localPosition = Vector3.Lerp(startPos, endPos, t);
			t += Time.deltaTime;
			yield return null;
		}
	}

	bool GameOver() {
		return phrases.Count == 0 && currentPhraseIndex == currentPhrase.textContent.Length;
	}

	IEnumerator ResetGame() {
		yield return new WaitForSeconds(4);
		UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
	}
}
