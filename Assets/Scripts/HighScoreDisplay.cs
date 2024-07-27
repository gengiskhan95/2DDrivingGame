using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HighScoreDisplay : MonoBehaviour
{
    public List<Text> highScoreTexts;

    void Start()
    {
        float currentScore = PlayerPrefs.GetFloat("CurrentScore", 0);
        string currentScoreText = currentScore.ToString();

        float highScore = PlayerPrefs.GetFloat("HighScore", 0);
        string highScoreText = highScore.ToString();

        foreach (var text in highScoreTexts)
        {
            if (text != null)
            {
                text.text = "High Score: " + highScoreText + "\nCurrent Score: " + currentScoreText;
            }
        }
    }
}
