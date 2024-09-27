using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelSelector : MonoBehaviour
{
    [SerializeField] private int levelIndex;
    private Button button;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SelectLevel);
    }

    private void SelectLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelIndex);
    }
}
