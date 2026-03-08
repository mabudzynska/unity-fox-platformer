using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    [Range(0.01f, 20.0f)] [SerializeField] private float moveSpeed = 0.1f;
    [SerializeField] private bool isFacingRight = true;   
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private float startPositionX;
    [SerializeField] private float moveRange = 1.0f;
    [SerializeField] private bool isMovingRight = true;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        startPositionX = this.transform.position.x;
        isMovingRight = true;
        isFacingRight = true;
    }
    
    void MoveRight()
    {
        transform.Translate(moveSpeed * Time.deltaTime, 0f, 0f, Space.World);
    }

    private void MoveLeft()
    {
        transform.Translate(-moveSpeed * Time.deltaTime, 0f, 0f, Space.World);
    }

    void FlipX()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    
    void Update()
    {
       if (isMovingRight)
        {
            MoveRight();

            if (transform.position.x >= startPositionX + moveRange)
            {
                isMovingRight = false;
                FlipX();
            }
        }
        else
        {
            MoveLeft();

            if (transform.position.x <= startPositionX - moveRange)
            {
                isMovingRight = true;
                FlipX();
            } 
        }
    }
    public IEnumerator KillOnAnimationEnd()
    {
        yield return new WaitForSeconds(1.0f);
        gameObject.SetActive(false);
    }
}
