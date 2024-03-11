using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _hiScoreText;

    private int _currentScore;
    private int _hiScore;

    private string _highScoreKey = "hiScore";

    private void Awake()
    {
        Enemy.OnDefeat += AddToScoreHelper;
        SetScore(0);
        LoadHighScore();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SetHighScore(0);
        }
    }

    private void AddToScoreHelper(Enemy enemy)
    {
        AddToScore(enemy.GetPoints());
    }

    private void AddToScore(int scoreToAdd)
    {
        _currentScore += scoreToAdd;
        string scoreFormatted = _currentScore.ToString("0000");
        _scoreText.text = $"SCORE\n {scoreFormatted}";

        if(_currentScore > _hiScore)
        {
            SetHighScore(_currentScore);
        }
    }

    private void SetScore(int score)
    {
        _currentScore = score;

        string scoreFormatted = _currentScore.ToString("0000");
        _scoreText.text = $"HI SCORE\n {scoreFormatted}";
    }

    private void SetHighScore(int score)
    {
        _hiScore = score;

        string scoreFormatted = _hiScore.ToString("0000");
        _hiScoreText.text = $"HI SCORE\n {scoreFormatted}";

        PlayerPrefs.SetInt(_highScoreKey, _hiScore);
    }

    private void LoadHighScore()
    {
        _hiScore = PlayerPrefs.GetInt(_highScoreKey);
        SetHighScore(_hiScore);
    }

    private void OnDestroy()
    {
        Enemy.OnDefeat -= AddToScoreHelper;
    }
}
