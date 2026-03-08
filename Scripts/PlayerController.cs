using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement parameters")]
    [Range(0.01f, 20.0f)] [SerializeField] private float moveSpeed = 0.1f;
    [SerializeField] private float jumpForce = 6.0f;
    [Space(10)]
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float rayLength = 0.5f;
    [SerializeField] private bool isGrounded;
    [SerializeField] private Animator animator;
    [SerializeField] private bool isRunning = false;
    [SerializeField] private bool isFacingRight = true;
    [SerializeField] private Vector2 spawnPosition;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spawnPosition = transform.position;
    }

    void Update()
    {
        if (GameManager.instance.currentState != GameState.GS_GAME)
        {
            return;
        }

        float move = Input.GetAxisRaw("Horizontal");

        if (move > 0 && !isFacingRight)
        {
            FlipX();
        }
        else if (move < 0 && isFacingRight)
        {
            FlipX();
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.Translate(moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.Translate(-moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
        }

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        Debug.DrawRay(transform.position, rayLength * Vector3.down, Color.white, 0.2f, false);

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        animator.SetBool("isGrounded", IsGrounded());
        animator.SetBool("isRunning", isRunning);
    }

    void FlipX()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, rayLength, groundLayer);
    }

    void Jump()
    {
        if (IsGrounded())
        {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            Debug.Log("jumping");
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("LevelExit"))
        {
            Debug.Log("Game Over");
        }

        if (col.CompareTag("LevelFall"))
        {
            Debug.Log("Game Over");
        }

        if (col.CompareTag("Bonus"))
        {
            GameManager.instance.AddPoints(10);
            col.gameObject.SetActive(false);
        }

        if (col.CompareTag("Enemy"))
        {
            if (transform.position.y > col.gameObject.transform.position.y)
            {
                Debug.Log("Killed an enemy");
                EnemyController enemyScript = col.gameObject.GetComponent<EnemyController>();

                if (enemyScript != null)
                {
                    StartCoroutine(enemyScript.KillOnAnimationEnd());
                }
            }
            else
            {
                Debug.Log("Game Over");
                transform.position = spawnPosition;
            }
        }

        if (col.CompareTag("MovingPlatform"))
        {
            transform.SetParent(col.transform);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("MovingPlatform"))
        {
            transform.SetParent(null);
        }
    }
}
