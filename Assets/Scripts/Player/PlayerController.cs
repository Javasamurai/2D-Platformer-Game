using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] public Rigidbody2D rigidbody2D;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask _groundLayer;

    [SerializeField] private ScoreController scoreController;
    [SerializeField] private HealthController healthController;
    [SerializeField] private GameOverController gameOverController;
    [SerializeField] private float speed;
    
    [Serializable]
    public struct PlayerState
    {
        public Transform transform;
        public bool isCrouching;
        public bool isGrounded;
        public bool midAir;
        public bool isJumping;
    }
    
    
    public PlayerState playerState;
    private AnimationController animationController;
    private PlayerPhysicsController playerPhysicsController;
    private static readonly int IsDead = Animator.StringToHash("isDead");

    private void Start()
    {
        playerState = new PlayerState
        {
            transform = transform
        };
        animationController = new AnimationController(animator, this);
        playerPhysicsController = new PlayerPhysicsController(this);
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Jump");
        
        PlayerMovement(horizontal, vertical);

        animationController.Update(playerState, horizontal, vertical);
        playerPhysicsController.Update();

        if (Input.GetKeyDown(KeyCode.S))
        {
            playerState.isCrouching = true;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            playerState.isCrouching = false;
        }

        if (!playerState.isGrounded && horizontal != 0)
        {
            playerPhysicsController.AirControl(horizontal);
        }
        
        if (transform.position.y < -10)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void FixedUpdate()
    {
        CheckIfGrounded();
    }

    private void CheckIfGrounded()
    {
        // RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.down), 10, _groundLayer);
        // Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 10, Color.red);
        // Debug.Log(hit.distance);
        // playerState.isGrounded = hit.collider != null && hit.distance <= 0;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            AudioManager.Instance.PlaySFX(SoundType.PLAYER_LAND);
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
    
    private void PlayerMovement(float horizontal, float vertical)
    {
        float horizontalMovement = horizontal * this.speed * Time.deltaTime;
        transform.position += new Vector3(horizontalMovement, 0, 0);
        playerState.midAir = rigidbody2D.velocity.y < 0;

        if (vertical > 0 && playerState.isGrounded)
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
        rigidbody2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        AudioManager.Instance.PlaySFX(SoundType.PLAYER_JUMP);
    }
    
    public void TakeDamage()
    {
        healthController.TakeDamage(1);
        if (healthController.GetHealth() <= 0)
        {
            rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
            rigidbody2D.velocity = Vector2.zero;
            transform.GetChild(0).SetParent(transform.parent);
            gameObject.SetActive(false);
            animator.SetBool(IsDead, true);
            gameOverController.ShowGameOverPanel();
        }
        else
        {
            animator.SetTrigger("hit");
        }
    }
}
