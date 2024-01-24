using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCompleteUI : MonoBehaviour
{
    [SerializeField] private GameObject levelCompletePanel;
    [SerializeField] private Button restartLevelButton;
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private Button menuButton;
    void Start()
    {
        nextLevelButton.onClick.AddListener(NextLevel);
        restartLevelButton.onClick.AddListener(RestartGame);
        menuButton.onClick.AddListener(GoToMainMenu);
    }

    private void NextLevel()
    {
        LevelManager.Instance.LoadNextLevel();
    }

    private void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ShowLevelCompleteUI()
    {
        levelCompletePanel.SetActive(true);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
