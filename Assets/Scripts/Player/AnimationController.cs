using System;
using UnityEngine;

public class AnimationController
{
    private Animator animator;
    private PlayerController playerController;

    public AnimationController(Animator _animator, PlayerController _playerController)
    {
        this.animator = _animator;
        this.playerController = _playerController;
    }
    
    public void Update(PlayerController.PlayerState playerState, float horizontal, float vertical)
    {
        PlayerAnimation(playerState, horizontal, vertical);
    }

    private void Flip()
    {
        var scale = Math.Abs(playerController.transform.localScale.x);
        playerController.transform.localScale = new Vector3(- scale, playerController.transform.localScale.y, playerController.transform.localScale.z);
    }
    
    private void PlayerAnimation(PlayerController.PlayerState playerState, float horizontal, float vertical)
    {
        Transform transform = playerState.transform;
        if (horizontal < 0)
        {
            Flip();
        }
        else if (horizontal > 0)
        {
            var scale = Math.Abs(transform.localScale.x);
            transform.localScale = new Vector3(scale, transform.localScale.y, transform.localScale.z);
        }
        animator.SetFloat("Speed", Mathf.Abs(horizontal));
        animator.SetBool("Crouch", playerState.isCrouching);
        animator.SetBool("Jump", !playerState.isGrounded);
        animator.SetBool("isGrounded", playerState.isGrounded);
    }
}