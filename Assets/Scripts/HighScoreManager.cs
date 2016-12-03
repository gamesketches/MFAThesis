using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HighScoreManager : MonoBehaviour {

	public class Entry {
		public string name;
		public int score;

		public Entry(string newName, int newScore) {
			name = newName;
			score = newScore;
		}

		public string GetEntry() {
			if(score != 0) {
				return string.Concat(name, " ", score.ToString());
			}
			else {
				return string.Concat(name, " ----");
			}
		}

		public string scoreVal() {
			return score.ToString();
		}
	}

	public Text HighScoreDisplay;
	public int numEntries = 10;
	public List<Entry> entries;

	// Use this for initialization
	void Start () {
		entries = new List<Entry>();
		string key;
		for(int i = 0; i < numEntries; i++) {
			key = "HighScore" + i.ToString();
			if(PlayerPrefs.HasKey(key + "score")) {
				entries.Add(new Entry(PlayerPrefs.GetString(key + "name"), PlayerPrefs.GetInt(key + "score")));//entries[i] = new Entry(PlayerPrefs.GetString(key + "name"), PlayerPrefs.GetInt(key + "score"));
			}
			else {
				entries.Add(new Entry(Random.value > 0.5 ? "Sam" : "Noca", 0));//entries[i] = new Entry(Random.value > 0.5 ? "Sam" : "Noca", 0);
				//break;
			}
		}
	}

	public void SetScores() {
		string key;
		for(int i = 0; i < numEntries; i++) {
			key = "HighScore" + i.ToString();
			PlayerPrefs.SetString(key + "name", entries[i].name);
			PlayerPrefs.SetInt(key + "score", entries[i].score);
		}
	}

	public void PrintScores() {
		string scoreList = "";
		foreach(Entry entry in entries) {
			scoreList = string.Concat(scoreList, entry.GetEntry(), "\n");
		}
		HighScoreDisplay.text = scoreList;
	}

	public void ClearLeaderBoard() {
		string key;
		for(int i = 0; i < numEntries; i++) {
			key = "HighScore" + i.ToString();
			PlayerPrefs.DeleteKey(key + "name");
			PlayerPrefs.DeleteKey(key + "score");
		}
	}

	void OnApplicationQuit()
	{
		PlayerPrefs.Save();
	}
	
	public bool MadeHighScoreList(int score) {
		return score > entries[numEntries -1].score;
	}

	public void CreateNewEntry(string name, int score) {
		Entry newChallenger = new Entry(name, score);
		for(int i = 0; i < entries.Count; i++) {
			if(entries[i].score < newChallenger.score) {
				entries.Insert(i, newChallenger);
				break;
			}
		}
		SetScores();
		entries.Remove(entries[numEntries]);
	}
}
