using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class Leaderboard : MonoBehaviour {

    public SortedDictionary<float, string> scores = new SortedDictionary<float, string>();
    public GameObject content;
    public GameObject singleScore;
    private TextMeshProUGUI placeTxt, nameTxt, scoreTxt;
    public int textSize = 30;

    public void addNewScore (string name, float score)
    {
        scores.Add(score, name);
    }

    public void setTestData(){
        //scores.Add(0, "");
    }
    public void loadDict() {
        ScoreboardData loadedData = ScoreSaveLoadManager.LoadScore();
        for(int i = 0; i < loadedData.length; i++){
              scores.Add(loadedData.scoreF[i], loadedData.names[i]);
        }
     
    }

    public void Start()
    {
        if (!ScoreSaveLoadManager.CheckFile())
        {
            setTestData();
            Save();
        }
    }

    public void Save(){
        
        ScoreSaveLoadManager.SaveScore(this);
    }

    public void Load(){
        ScoreboardData loadedData = ScoreSaveLoadManager.LoadScore();
       
    }

    public void fillList()
    {
        loadDict();

        int i = 1;
        foreach (var pair in scores.Reverse())
        {
            int scoreInt;
            scoreInt = (int)pair.Key;
            GameObject currentScore = Instantiate(singleScore);

            nameTxt = (TextMeshProUGUI)currentScore.transform.Find("Name").GetComponent("TextMeshProUGUI");
            placeTxt = (TextMeshProUGUI)currentScore.transform.Find("Place").GetComponent("TextMeshProUGUI");
            scoreTxt = (TextMeshProUGUI)currentScore.transform.Find("Score").GetComponent("TextMeshProUGUI");
            placeTxt.SetText(i + ".");
            nameTxt.SetText(pair.Value.ToString());
            scoreTxt.SetText(scoreInt.ToString());

            currentScore.name = "Scorer_" + i;
            currentScore.transform.SetParent(content.transform);
            currentScore.transform.localScale = (new Vector3Int(1, 1, 1));
            currentScore.transform.localPosition = (new Vector3Int(0, -(textSize * (i-1)), 0));
            i++;
        }
        RectTransform rt = content.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(0, (i * textSize) - textSize);
    }

    public void clearList()
    {
        foreach (Transform child in content.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        scores.Clear();
    }
}

//TODO:
// Persistenz