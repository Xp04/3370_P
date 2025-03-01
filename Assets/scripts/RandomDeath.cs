using System.Collections;
using System.Collections.Generic;
using UnityEngine;  

public class RandomDeath : MonoBehaviour
{
    
    public float speed = 3f; // Speed of movement
    public float changeDirectionTime = 2f; // Time between direction changes
    public Vector2 minBounds; // Bottom Left corner boundary
    public Vector2 maxBounds; // Top Right corner boundary

    private Vector2 moveDirection;

    void Start()
    {
        StartCoroutine(ChangeDirection());
    }

    void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);

        // Keep the object inside the boundary
        KeepWithinBounds();

        // Force Z-position to stay at -1
        transform.position = new Vector3(transform.position.x, transform.position.y, -1);
    }

    IEnumerator ChangeDirection()
    {
        while (true)
        {
            // Pick a random direction (X and Y between -1 and 1)
            moveDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

            yield return new WaitForSeconds(changeDirectionTime);
            speed += 0.1f;
        }
    }

    void KeepWithinBounds()
    {
        // Clamping X position
        float clampedX = Mathf.Clamp(transform.position.x, minBounds.x, maxBounds.x);
        // Clamping Y position
        float clampedY = Mathf.Clamp(transform.position.y, minBounds.y, maxBounds.y);

        transform.position = new Vector2(clampedX, clampedY);

        // If the object reaches the boundary, reverse direction
        if (transform.position.x == minBounds.x || transform.position.x == maxBounds.x)
        {
            moveDirection.x *= -1;
        }
        if (transform.position.y == minBounds.y || transform.position.y == maxBounds.y)
        {
            moveDirection.y *= -1;
        }
    }
}
