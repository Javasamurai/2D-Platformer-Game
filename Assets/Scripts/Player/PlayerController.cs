using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private float jumpForce;
    [SerializeField] private ScoreController scoreController;
    [SerializeField] private HealthController healthController;
    [SerializeField] private GameOverController gameOverController;
    [SerializeField] private float speed;
    
    public struct PlayerState
    {
        public Transform transform;
        public bool isCrouching;
        public bool isGrounded;
        public bool midAir;
    }
    
    private PlayerState playerState;
    private AnimationController animationController;

    [SerializeField] private float maxJumpHeight = 2f;
    private float currentPlatformHeight = 0f;
    
    private void Start()
    {
        playerState = new PlayerState
        {
            transform = transform
        };
        animationController = new AnimationController(animator, this);
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Jump");
        
        PlayerMovement(horizontal, vertical);
        if (playerState.isGrounded)
        {
            currentPlatformHeight = transform.position.y;
        }
        CheckIfPlayerFell();
        animationController.Update(playerState, horizontal, vertical);
        if (Input.GetKeyDown(KeyCode.S))
        {
            playerState.isCrouching = true;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            playerState.isCrouching = false;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            playerState.isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            playerState.isGrounded = false;
        }
    }

    public void PickUpKey()
    {
        scoreController.AddScore(10);
    }

    private void CheckIfPlayerFell()
    {
        if (transform.position.y < currentPlatformHeight - 10)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    
    private void PlayerMovement(float horizontal, float vertical)
    {
        float horizontalMovement = horizontal * this.speed * Time.deltaTime;
        transform.position += new Vector3(horizontalMovement, 0, 0);
        playerState.midAir = rigidbody2D.velocity.y < 0;;

        if (vertical > 0 && !playerState.midAir && transform.position.y < currentPlatformHeight + maxJumpHeight)
        {
            Jump();
        }
        if ((Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl)))
        {
            playerState.isCrouching = !playerState.isCrouching;
        }
    }
    
    private void Jump()
    {
        playerState.isCrouching = false;
        rigidbody2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Force);
    }
    
    public void TakeDamage()
    {
        healthController.TakeDamage(1);
        if (healthController.GetHealth() <= 0)
        {
            gameOverController.ShowGameOverPanel();
            rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
            rigidbody2D.velocity = Vector2.zero;
            animator.SetBool("isDead", true);
        }
    }
}
