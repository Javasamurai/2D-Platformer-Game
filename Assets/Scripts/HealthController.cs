using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [SerializeField] List<Image> hearts;
    [SerializeField] Sprite fullHeart;
    [SerializeField] Sprite emptyHeart;
    private float health = 3;
    private void Start()
    {
        UpdateHealth(hearts.Count);
    }
    
    public float GetHealth()
    {
        return health;
    }
    
    public void TakeDamage(int damage)
    {
        health -= damage;
        UpdateHealth((int)health);
    }
    
    private void UpdateHealth(int health)
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
}
