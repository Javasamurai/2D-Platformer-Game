using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string[] levelNames;
    
    private static LevelManager instance;
    public static LevelManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        if (GetLevelState(levelNames[0]) == LevelState.LOCKED)
        {
            SetLevelState(levelNames[0], LevelState.UNLOCKED);
        }
    }
    
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
    
    public void SetLevelState(string levelName, LevelState levelState)
    {
        PlayerPrefs.SetInt(levelName, (int) levelState);
    }

    public LevelState GetLevelState(string levelName)
    {
        return (LevelState) PlayerPrefs.GetInt(levelName, 0);
    }

    public void SetCurrentLevelCompleted()
    {
        var currentLevel = SceneManager.GetActiveScene().name;
        
        SetLevelState(currentLevel, LevelState.COMPLETED);
        string nextLevel = GetNextLevel(currentLevel);

        if (nextLevel != null)
        {
            SetLevelState(nextLevel, LevelState.UNLOCKED);
        }
    }
    
    public void LoadNextLevel()
    {
        var currentLevel = SceneManager.GetActiveScene().name;
        string nextLevel = GetNextLevel(currentLevel);

        if (nextLevel != null)
        {
            LoadLevel(nextLevel);
        }
    }

    private string GetNextLevel(string currentLevel)
    {
        for (int i = 0; i < levelNames.Length; i++)
        {
            if (levelNames[i] == currentLevel)
            {
                if (i + 1 < levelNames.Length)
                {
                    return levelNames[i + 1];
                }
            }
        }

        return null;
    }
}