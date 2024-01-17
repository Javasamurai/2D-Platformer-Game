using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private bool isCrouching = false;
    
    void Update()
    {
        float speed = Input.GetAxis("Horizontal");
        
        animator.SetFloat("Speed", Mathf.Abs(speed));
        bool isFlipped = speed < 0;
        float x = (isFlipped ? -1 : 1) * Mathf.Abs(transform.localScale.x);
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
        if (Input.GetKeyDown(KeyCode.C) && speed == 0)
        {
            isCrouching = !isCrouching;
            animator.SetBool("Crouch", isCrouching);
        }
    }
}
