using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelSelector : MonoBehaviour
{
    [SerializeField] private string levelName;
    private Button button;
    private LevelState levelState;

    void Start()
    {
        button = GetComponent<Button>();
        levelState = LevelManager.Instance.GetLevelState(levelName);
        button.onClick.AddListener(SelectLevel);
        if (levelState == LevelState.LOCKED)
        {
            button.interactable = false;
        }
    }

    private void SelectLevel()
    {
        if (levelState != LevelState.LOCKED)
        {
            LevelManager.Instance.LoadLevel(levelName);
        }
    }
}
