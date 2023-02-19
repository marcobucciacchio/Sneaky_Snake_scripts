using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    public int highScore = 10000;
    Text highScoreText;

    // Start is called before the first frame update
    void Start()
    {
        highScoreText = GetComponent<Text>();
        PlayerPrefs.SetInt("HighScore", highScore);
        highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text += " " + highScore;
        SaveHighScore();
    }


    public void SaveHighScore()
    {
        if (Score.scoreValue > highScore)
        {
            highScore = Score.scoreValue; 
        }
    }
}
