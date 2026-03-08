using UnityEngine;

public class CircularPlatformMover : MonoBehaviour
{
    private const int PLATFORMS_NUM = 14;

    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private float radius = 1.9f;
    [SerializeField] private float rotationSpeed = 30f;

    private GameObject[] platforms;

    void Awake()
    {
        platforms = new GameObject[PLATFORMS_NUM];

        for (int i = 0; i < PLATFORMS_NUM; i++)
        {
            GameObject platform = Instantiate(platformPrefab, Vector3.zero, Quaternion.identity);
            platforms[i] = platform;
        }
    }

    void Update()
    {
        float angleOffset = Time.time * rotationSpeed;

        for (int i = 0; i < PLATFORMS_NUM; i++)
        {
            float angle = i * 360f / PLATFORMS_NUM + angleOffset;
            float rad = angle * Mathf.Deg2Rad;

            float x = transform.position.x + Mathf.Cos(rad) * radius;
            float y = transform.position.y + Mathf.Sin(rad) * radius;

            platforms[i].transform.position = new Vector2(x, y);
        }
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
