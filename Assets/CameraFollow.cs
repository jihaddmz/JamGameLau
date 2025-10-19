using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform player; // The player to follow
    public float smoothSpeed = 0.125f; // Smoothness of the follow
    public Vector3 offset; // Offset between camera and player

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player == null)
            return;

        // Desired position of the camera
        Vector3 desiredPosition = player.position + offset;

        // Smoothly move camera to target position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;
    }
}
