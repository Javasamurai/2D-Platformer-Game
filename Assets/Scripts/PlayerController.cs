using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private float jumpForce;

    [SerializeField] private float speed;
    // [SerializeField] private BoxCollider2D boxCollider2D;

    private bool isCrouching = false;
    // private float crouchOffset = 0.5f;
    // private float crouchSize = 0.5f;
    
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Jump");
        
        PlayerMovement(horizontal, vertical);
        PlayerAnimation(horizontal, vertical);
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
        bool isGrounded = Mathf.Abs(rigidbody2D.velocity.y) < 0.001f;
        animator.SetBool("Jump", !isGrounded);
        // animator.SetBool("isGrounded", Mathf.Abs(rigidbody2D.velocity.y) < 0.001f);
    }
    
    private void PlayerMovement(float horizontal, float vertical)
    {
        float horizontalMovement = horizontal * this.speed * Time.deltaTime;
        transform.position += new Vector3(horizontalMovement, 0, 0);
        if (vertical > 0 && Mathf.Abs(rigidbody2D.velocity.y) < 0.001f)
        {
            rigidbody2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Force);
        }
    }
}
