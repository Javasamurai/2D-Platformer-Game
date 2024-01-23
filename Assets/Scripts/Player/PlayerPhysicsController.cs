using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPhysicsController
{
    private LayerMask groundLayer = LayerMask.GetMask("Ground");
    private PlayerController playerController;
    
    public PlayerPhysicsController(PlayerController _playerController)
    {
        this.playerController = _playerController;
    }

    public void Update()
    {
        
    }

    public void AirControl(float horizontal)
    {
        playerController.rigidbody2D.AddForce(new Vector2(horizontal * 10, 0));
    }
}
