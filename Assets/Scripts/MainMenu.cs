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
        closeLevelSelectorButton.onClick.AddListener(() => levelSelector.SetActive(false));
        quitButton.onClick.AddListener(QuitGame);
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    private void StartGame()
    {
        levelSelector.SetActive(true);
    }
}
