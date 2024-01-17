using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private BoxCollider2D boxCollider2D;
    private bool isCrouching = false;
    private float crouchOffset = 0.5f;
    private float crouchSize = 0.5f;
    
    void Update()
    {
        float speed = Input.GetAxis("Horizontal");
        
        animator.SetFloat("Speed", Mathf.Abs(speed));
        bool isFlipped = speed < 0;
        float x = (isFlipped ? -1 : 1) * Mathf.Abs(transform.localScale.x);
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
        if ((Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl)) && speed == 0)
        {
            isCrouching = !isCrouching;
            animator.SetBool("Crouch", isCrouching);
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
    }
}
