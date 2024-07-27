using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    [Header("UI Components")]
    public Text scoreText;
    public Text highScoreText;

    [Header("Score Settings")]
    public float currentScore;
    public float scoreIncrement = 10f;

    public static ScoreManager Instance { get; private set; }

    [System.Serializable]
    public class ScoreEvent : UnityEvent<float> { }

    public ScoreEvent OnScoreChanged;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (OnScoreChanged == null)
        {
            OnScoreChanged = new ScoreEvent();
        }
    }

    void Start()
    {
        UpdateScoreText();
        UpdateHighScoreText();
    }

    void Update()
    {
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = currentScore.ToString();
        }
    }

    private void UpdateHighScoreText()
    {
        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + GetHighScore().ToString();
        }
    }

    public void IncreaseScore(float amount)
    {
        currentScore += amount;
        PlayerPrefs.SetFloat("CurrentScore", currentScore);
        PlayerPrefs.Save();
        UpdateScoreText();
        OnScoreChanged.Invoke(currentScore);
        CheckHighScore();
    }

    public void ResetScore()
    {
        currentScore = 0;
        PlayerPrefs.SetFloat("CurrentScore", currentScore);
        PlayerPrefs.Save();
        UpdateScoreText();
    }

    private void CheckHighScore()
    {
        float highScore = PlayerPrefs.GetFloat("HighScore", 0);

        if (currentScore > highScore)
        {
            PlayerPrefs.SetFloat("HighScore", currentScore);
            PlayerPrefs.Save();
            UpdateHighScoreText();
        }
    }

    public float GetHighScore()
    {
        return PlayerPrefs.GetFloat("HighScore", 0);
    }

    public void SaveHighScore()
    {
        CheckHighScore();
        PlayerPrefs.SetFloat("CurrentScore", currentScore);
        PlayerPrefs.Save();
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(ScoreManager))]
public class ScoreManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        ScoreManager scoreManager = (ScoreManager)target;

        if (GUILayout.Button("Increase Score"))
        {
            scoreManager.IncreaseScore(scoreManager.scoreIncrement);
        }

        if (GUILayout.Button("Reset Score"))
        {
            scoreManager.ResetScore();
        }
    }
}
#endif
