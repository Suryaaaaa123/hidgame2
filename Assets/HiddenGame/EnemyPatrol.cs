using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] patrolPoints; // Array of patrol points
    public float speed = 2f; // Speed of the enemy
    private int currentPointIndex; // Current patrol point index
    private Transform targetPoint; // Target patrol point

    void Start()
    {
        if (patrolPoints.Length > 0)
        {
            currentPointIndex = 0;
            targetPoint = patrolPoints[currentPointIndex];
        }
    }

    void Update()
    {
        if (patrolPoints.Length > 0)
        {
            Patrol();
        }
    }

    void Patrol()
    {
        // Move towards the target point
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        // Check if the enemy has reached the target point
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.2f)
        {
            // Move to the next patrol point
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
            targetPoint = patrolPoints[currentPointIndex];
        }
    }
}
