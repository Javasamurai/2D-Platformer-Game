using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private float jumpForce;

    [SerializeField] private float speed;

    private bool isCrouching = false;
    private bool isGrounded => Math.Abs(rigidbody2D.velocity.y) < 0.001f;
    private bool midAir = false;
    [SerializeField] private float maxJumpHeight = 2f;
    private float currentPlatformHeight = 0f;

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Jump");
        
        PlayerMovement(horizontal, vertical);
        PlayerAnimation(horizontal, vertical);
        if (isGrounded)
        {
            currentPlatformHeight = transform.position.y;
        }
        CheckIfPlayerFell();
    }

    private void CheckIfPlayerFell()
    {
        if (transform.position.y < currentPlatformHeight - 10)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void PlayerAnimation(float horizontal, float vertical)
    {
        bool isFlipped = horizontal < 0;
        float x = (isFlipped ? -1 : 1) * Mathf.Abs(transform.localScale.x);
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
        animator.SetFloat("Speed", Mathf.Abs(horizontal));
        if ((Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl)) && speed == 0)
        {
            isCrouching = !isCrouching;
            animator.SetBool("Crouch", isCrouching);
            
            // Both ways are working
            // 1. Using animation and adjusting collider
            // 2. Using code and adjusting box collider size and offset
            // if (isCrouching)
            // {
            //     boxCollider2D.size = new Vector2(boxCollider2D.size.x, boxCollider2D.size.y * crouchSize);
            //     boxCollider2D.offset = new Vector2(boxCollider2D.offset.x, boxCollider2D.offset.y - crouchOffset);
            // }
            // else
            // {
            //     boxCollider2D.size = new Vector2(boxCollider2D.size.x, boxCollider2D.size.y / crouchSize);
            //     boxCollider2D.offset = new Vector2(boxCollider2D.offset.x, boxCollider2D.offset.y + crouchOffset);
            // }
        }
        animator.SetBool("Jump", !isGrounded);
        animator.SetBool("isGrounded", isGrounded);
    }
    
    private void PlayerMovement(float horizontal, float vertical)
    {
        float horizontalMovement = horizontal * this.speed * Time.deltaTime;
        transform.position += new Vector3(horizontalMovement, 0, 0);
        
        midAir = rigidbody2D.velocity.y < 0;

        if (vertical > 0 && !midAir && transform.position.y < currentPlatformHeight + maxJumpHeight)
        {
            Jump();
        }
    }
    
    private void Jump()
    {
        rigidbody2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Force);
    }
}
