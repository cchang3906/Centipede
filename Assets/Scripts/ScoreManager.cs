using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    int score = 0;
    int highScore = 0;
    private int lifeIncreaseCounter = 0;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        highScore = PlayerPrefs.GetInt("highscore", 0);
        scoreText.text = score.ToString();
        highScoreText.text = highScore.ToString();
    }

    // Update is called once per frame
    public void AddPoints(int points)
    {
        /*
        if (i == "centipede")
        {
            score += 100;
        }
        else if (i == "spider")
        {
            score += 300;
        }
        */
        score += points;
        
        if (FindObjectOfType<GameManager>().lifeCount < 3)
        {
            lifeIncreaseCounter += points;
        }
        else
        {
            lifeIncreaseCounter = 0;
        }

        if( highScore < score)
        {
            PlayerPrefs.SetInt("highscore", score);
        }
        if(lifeIncreaseCounter > 7000 && FindObjectOfType<GameManager>().lifeCount < 3)
        {
            FindObjectOfType<GameManager>().gainLife();
            lifeIncreaseCounter -= 7000;
        }
        scoreText.text = score.ToString();
    }
}
