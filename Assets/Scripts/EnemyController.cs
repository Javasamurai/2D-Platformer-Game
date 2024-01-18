using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<PlayerController>() != null)
        {
            Debug.Log("Player hit enemy");
            var playerController = other.gameObject.GetComponent<PlayerController>();
            playerController.Die();
        }
    }
}
