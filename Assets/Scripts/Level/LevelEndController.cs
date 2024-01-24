using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndController : MonoBehaviour
{
    [SerializeField] private LevelCompleteUI levelCompleteUI;
    [SerializeField] private AnimatedSprites animatedSprites;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LevelManager.Instance.SetCurrentLevelCompleted();
            levelCompleteUI.ShowLevelCompleteUI();
            // Freeze player
            var playerController = other.GetComponent<PlayerController>();
            playerController.Freeze();
            animatedSprites.enabled = true;
        }
    }
}
