using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPhysicsController
{
    private LayerMask groundLayer = LayerMask.GetMask("Ground");
    private PlayerController playerController;
    private float glideForce = 100f;
    
    public PlayerPhysicsController(PlayerController _playerController)
    {
        this.playerController = _playerController;
    }

    public void Glide()
    {
        var forceDirection = playerController.playerState.isFlipped ? -1 : 1;
        playerController.rigidbody2D.AddForce(new Vector2(forceDirection * glideForce, 0), ForceMode2D.Impulse);
    }
}
