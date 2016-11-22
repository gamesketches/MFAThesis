using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MeditativeMashing : MonoBehaviour {

	public Text instructions;
	public Text currentMashText;
	public Image healthBar;
	public Image noiseBar;
	public Text timer;
	public float time;
	KeyCode[] inputs;
	public float health;
	public float deltaHealth;
	public float healthGain;
	private float startingHealth;
	MicInput mic;
	// Use this for initialization
	void Start () {
		startingHealth = health;
		NewInputKeys();
		mic = GetComponent<MicInput>();
		Invoke("NewInputKeys", Random.Range(0, 10));
		Invoke("NewInputKeys", Random.Range(11, 20));
		Invoke("NewInputKeys", Random.Range(21, 30));
	}
	
	// Update is called once per frame
	void Update () {
		if(health > 0 && time > 0) {
			time -= Time.deltaTime;
			timer.text = Mathf.RoundToInt(time).ToString();
			health -= (deltaHealth * Time.deltaTime);
			foreach(KeyCode key in inputs) {
				if(Input.GetKeyUp(key)) {
					health += healthGain;
				}
			}
			if(mic.MicLoudness > 0.1f){
				Debug.Log("demerit");
				health -= 10;
			}
	
			noiseBar.rectTransform.localScale = new Vector3(0.5f, mic.MicLoudness, 1);
			healthBar.rectTransform.localScale = new Vector3(0.5f, health / startingHealth, 1);
			Debug.Log(health);
		}
		else if(health < 0){
			instructions.text = "YOU LOSE :( :( :(";
		}
		else if(time < 0) {
			instructions.text = "YOU WIN :D :D :D";
		}
	}

	void NewInputKeys() {
		inputs = new KeyCode[] {RandomKeyCode(),
									 RandomKeyCode(), RandomKeyCode(), RandomKeyCode()};
		currentMashText.text = inputs[0].ToString() + " " + inputs[1].ToString() + " " + inputs[2].ToString() + " " + inputs[3].ToString();
	}
	KeyCode RandomKeyCode() {
		string st = "abcdefghijklmnopqrstuvwxyz";
		Debug.Log("generating a keycode");
		return (KeyCode)System.Enum.Parse(typeof(KeyCode), st[Random.Range(0, st.Length)].ToString().ToUpper());
	}
}
