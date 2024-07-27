using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarMobileController : MonoBehaviour
{
    [Header("Speed Settings")]
    public float verticalSpeedMultiplier = 1.0f;
    public float baseSpeed = 5.0f;
    public float baseSpeedFactor = 50f;
    public float horizontalSmoothTime = 0.2f;

    [Header("Movement Limits")]
    public float rightBoundary = 5.0f;
    public float leftBoundary = -5.0f;

    [Header("Target Offset")]
    public float targetOffset = 1.0f;

    private Vector3 targetPosition;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        ManageControl();
    }

    void FixedUpdate()
    {
        MoveForward();
        SmoothMove();
        ClampPosition();
    }

    private void MoveForward()
    {
        float verticalVelocity = baseSpeed * baseSpeedFactor * Time.fixedDeltaTime;
        transform.position += Vector3.up * verticalVelocity;
    }

    private void ManageControl()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = transform.position.z;
            targetPosition = mousePosition + Vector3.up * targetOffset;
        }
    }

    private void SmoothMove()
    {
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, horizontalSmoothTime);
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
