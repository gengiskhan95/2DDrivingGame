using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarPCController : MonoBehaviour
{
    [Header("Speed Settings")]
    public float verticalSpeedMultiplier = 1.0f;
    public float horizontalSpeedMultiplier = 1.0f;
    public float baseSpeed = 5.0f;
    public float baseSpeedFactor = 50f;

    [Header("Movement Limits")]
    public float rightBoundary = 5.0f;
    public float leftBoundary = -5.0f;

    private float verticalInput;
    private float horizontalInput;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        ProcessMovement();
        ClampPosition();
    }

    private void ProcessMovement()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        float horizontalVelocity = horizontalInput * baseSpeedFactor * horizontalSpeedMultiplier * Time.fixedDeltaTime;
        float verticalVelocity;

        if (verticalInput < 0)
        {
            verticalVelocity = Mathf.Max(baseSpeed * baseSpeedFactor * Time.fixedDeltaTime, verticalInput * baseSpeedFactor * verticalSpeedMultiplier * Time.fixedDeltaTime);
        }
        else
        {
            verticalVelocity = (baseSpeed * baseSpeedFactor + verticalInput * baseSpeedFactor * verticalSpeedMultiplier) * Time.fixedDeltaTime;
        }

        rb.velocity = new Vector2(horizontalVelocity, verticalVelocity);
    }


    private void ClampPosition()
    {
        Vector3 clampedPosition = transform.position;

        if (clampedPosition.x > rightBoundary)
        {
            clampedPosition.x = rightBoundary;
        }
        else if (clampedPosition.x < leftBoundary)
        {
            clampedPosition.x = leftBoundary;
        }

        transform.position = clampedPosition;
    }
}
