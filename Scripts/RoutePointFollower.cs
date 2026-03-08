using UnityEngine;

public class PlatformRouteMover : MonoBehaviour
{
    [SerializeField] private GameObject[] RoutepointArray;
    [SerializeField] private float speed = 1.0f;
    private int currentRoutepoint = 0;

    void Update()
    {
        if (RoutepointArray.Length == 0) return;

        Vector2 currentPosition = transform.position;
        Vector2 targetPosition = RoutepointArray[currentRoutepoint].transform.position;

        float distance = Vector2.Distance(currentPosition, targetPosition);

        if (distance < 0.1f)
        {
            currentRoutepoint = (currentRoutepoint + 1) % RoutepointArray.Length;
        }

        Vector2 newPosition = Vector2.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime);
        transform.position = newPosition;
    }
}
