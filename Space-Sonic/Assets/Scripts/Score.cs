using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour {

	public int score = 0;
	private int highscore = 0;
	public TMP_Text scoreText;

	void Start()
	{
		if (PlayerPrefs.GetInt ("Score") <= 0) {
			score = 0;
		} else {
			score = PlayerPrefs.GetInt("Score");
		}
		if (PlayerPrefs.GetString ("Level") == "") {
			PlayerPrefs.SetString("Level", Application.loadedLevelName);		
		}

		highscore = PlayerPrefs.GetInt ("Best");

	}

	 public void AddPoint()
	{
		score++;
		if (score > highscore) {
			highscore = score;
		}
	}

	void OnDestroy()
	{
		PlayerPrefs.SetInt("Best", highscore);
	
	}

	void Update()
	{
		scoreText.text = "Score : " + score + "\nHighScore : "+highscore;
	}
}
