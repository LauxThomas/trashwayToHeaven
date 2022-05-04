using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

	public Slider playerSlider, waterSlider;
	public InputField name;
	public highscoreController score;
	public Leaderboard board;
	public GameObject EndPanel, ReplayScreen;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	//TODO: write Method for when replay button clicked
	//get object von wos aufgerufen wird
	
	public void returnToMenu () {
		if (EndPanel.activeInHierarchy)
		{
			if (score.getHighscore() >= 0 && name.text != "")
			{
				board.loadDict();
				board.scores.Add(score.getHighscore(), name.text);
				board.Save();
				SceneManager.LoadScene(0);
			}
		}
	}

	public void restartMatch () {
		SceneManager.LoadScene(1);
	}






}
