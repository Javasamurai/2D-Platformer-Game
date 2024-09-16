using System;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    private int score = 0;

    private void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        UpdateUI();
    }

    private void UpdateUI()
    {
        scoreText.text = string.Format("Score: {0}", score);
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        UpdateUI();
    }
}