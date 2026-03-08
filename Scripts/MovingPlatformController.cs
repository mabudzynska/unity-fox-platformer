using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    [Range(0.01f, 20.0f)] [SerializeField] private float moveSpeed = 1.0f;
    [SerializeField] private float moveRange = 1.0f;
    [SerializeField] private float startPositionX;
    [SerializeField] private bool isMovingRight = true;

    void Awake()
    {
        startPositionX = transform.position.x;
        isMovingRight = true;
    }

    void Update()
    {
        if (isMovingRight)
        {
            MoveRight();

            if (transform.position.x >= startPositionX + moveRange)
            {
                isMovingRight = false;
            }
        }
        else
        {
            MoveLeft();

            if (transform.position.x <= startPositionX - moveRange)
            {
                isMovingRight = true;
            }
        }
    }

    void MoveRight()
    {
        transform.Translate(moveSpeed * Time.deltaTime, 0f, 0f, Space.World);
    }

    void MoveLeft()
    {
        transform.Translate(-moveSpeed * Time.deltaTime, 0f, 0f, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Gracz wszedł na platformę");
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Gracz stoi na platformie");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Gracz opuścił platformę");
        }
    }
}
