using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Button startButton;
    [SerializeField]
    private Button quitButton;
    [SerializeField]
    private Button closeLevelSelectorButton;
    [SerializeField]
    private GameObject levelSelector;

    void Start()
    {
        startButton.onClick.AddListener(StartGame);
        closeLevelSelectorButton.onClick.AddListener(ShowLevelSelector);
        quitButton.onClick.AddListener(QuitGame);
    }
    
    private void ShowLevelSelector()
    {
        AudioManager.Instance.PlaySFX(SoundType.BUTTON_CLICK);
        levelSelector.SetActive(false);
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    private void StartGame()
    {
        AudioManager.Instance.PlaySFX(SoundType.BUTTON_CLICK);
        levelSelector.SetActive(true);
    }

    private void OnDestroy()
    {
        startButton.onClick.RemoveListener(StartGame);
        quitButton.onClick.RemoveListener(QuitGame);
        closeLevelSelectorButton.onClick.RemoveListener(ShowLevelSelector);
    }
}
