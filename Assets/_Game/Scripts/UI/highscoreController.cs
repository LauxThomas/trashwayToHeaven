using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class highscoreController : MonoBehaviour
{
    public float highscore;
    private float highscoreHelper;
    private bool isAlive;
    private float timeComponent;

    // Use this for initialization
    void Start ()
    {
        isAlive = true;
    }

    // Update is called once per frame
    void Update ()
    {
        if(isAlive)
        highscore = 5*GameObject.Find("Player").transform.position.y;
        highscore = Mathf.Max(highscore, highscoreHelper);
        highscoreHelper = highscore;
        timeComponent += Time.deltaTime * 0.1f;

    }

    public float getHighscore()
    {
        isAlive = false;
        highscore -= timeComponent;
        return highscore;
    }
}