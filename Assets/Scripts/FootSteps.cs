using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    public float stepRate = 0.1f;
    private float lastStep = 0f;
    
    public void PlayFootStep()
    {
        if (Time.time - lastStep < stepRate)
        {
            return;
        }
        lastStep = Time.time;
        audioSource.clip = audioClips[UnityEngine.Random.Range(0, audioClips.Length)];
        audioSource.Play();
    }
}
