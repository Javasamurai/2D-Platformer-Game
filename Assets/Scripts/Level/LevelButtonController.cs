using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonController : MonoBehaviour
{
    [SerializeField] private string levelName;
    private Button button;
    private LevelState levelState;

    private void Awake()
    {
        button = GetComponent<Button>();
        levelState = LevelManager.Instance.GetLevelState(levelName);
    }
}
