using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button menuButton;
    void Awake()
    {
        restartButton.onClick.AddListener(RestartGame);
        menuButton.onClick.AddListener(GoToMainMenu);
    }

    private void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnDestroy()
    {
        restartButton.onClick.RemoveListener(RestartGame);
        menuButton.onClick.RemoveListener(GoToMainMenu);
    }
}
