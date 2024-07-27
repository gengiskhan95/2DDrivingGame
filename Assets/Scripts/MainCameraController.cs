using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    public Transform playerCar;
    public float offsetY = 5.0f;
    public float smoothSpeed = 0.125f;

    private float initialX;
    private float initialZ;

    void Start()
    {
        initialX = transform.position.x;
        initialZ = transform.position.z;
    }

    void FixedUpdate()
    {
        if (playerCar == null)
        {
            //Debug.LogWarning("PlayerCar is not assigned!");
            return;
        }

        Vector3 desiredPosition = new Vector3(initialX, playerCar.position.y + offsetY, initialZ);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
