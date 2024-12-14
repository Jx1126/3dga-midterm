using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    public static ScoreScript instance;
    public TMP_Text scoreText; 
    public TMP_Text highScoreText;

    int score = 0;
    int gameScore = 0;
    int highScore = 0;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        gameScore = PlayerPrefs.GetInt("GameScore", 0);
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        scoreText.text = "SCORE: " + score;
        highScoreText.text = "HIGHSCORE: " + highScore;
    }

    public void AddScore()
    {
        score ++;
        scoreText.text = "SCORE: " + score;

        if (score != gameScore)
        {
            PlayerPrefs.SetInt("GameScore", score);
        }

        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }

    public int GetCurrentScore()
    {
        return score;
    }
}