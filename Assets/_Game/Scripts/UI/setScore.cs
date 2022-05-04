using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class setScore : MonoBehaviour
{

	public highscoreController score;
	public TextMeshProUGUI scoreField;

	private void OnEnable()
	{
		scoreField.SetText((int)score.getHighscore() + "");
	}
}
