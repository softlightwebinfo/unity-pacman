using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UiManager : MonoBehaviour
{
    public static UiManager sharedInstance;
    public Text titleLabel;
    public Text scoreLabel;
    private int totalScore;

    private void Awake()
    {
        if (!sharedInstance) sharedInstance = this;
        this.totalScore = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.sharedInstance.gamePaused || !GameManager.sharedInstance.gameStarted)
        {
            this.titleLabel.enabled = true;
        }
        else
        {
            this.titleLabel.enabled = false;
        }
    }

    public void ScorePoints(int points)
    {
        this.totalScore += points;
        this.scoreLabel.text = "Score: " + this.totalScore.ToString();
    }
}
