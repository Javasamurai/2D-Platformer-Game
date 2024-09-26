using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndController : MonoBehaviour
{
    [SerializeField] private LevelCompleteUI levelCompleteUI;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LevelManager.Instance.SetCurrentLevelCompleted();
            levelCompleteUI.ShowLevelCompleteUI();
        }
    }
}
