using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float patrolSpeed = 3f;
    [SerializeField] private float patrolDistance = 3f;

    private Vector3 initialPosition;
    private Vector3 direction = Vector3.right;
    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        Patrol();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<PlayerController>() != null)
        {
            Debug.Log("Player hit enemy");
            var playerController = other.gameObject.GetComponent<PlayerController>();
            playerController.TakeDamage();
        }
    }
    
    private void Flip()
    {
        direction = -direction;
        transform.Rotate(0f, 180f, 0f);
    }
    
    private void Patrol()
    {
        var distance = Vector3.Distance(transform.position, initialPosition);
        if (distance > patrolDistance)
        {
            Flip();
        }
        transform.position += direction * (patrolSpeed * Time.deltaTime);
    }
}
