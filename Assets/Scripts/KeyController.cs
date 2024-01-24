using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<PlayerController>() != null)
        {
            var playerController = other.gameObject.GetComponent<PlayerController>();
            playerController.PickUpKey();
            AudioManager.Instance.PlaySFX(SoundType.KEY_PICKUP);
            Destroy(gameObject);
        }
    }
}
