using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player; // Reference to the player
    public Transform cameraTransform; // Reference to the camera
    public float sensitivity = 100f; // Sensitivity for camera movement
    public float distanceFromPlayer = 3f; // Camera distance from player
    public Vector2 verticalLimits = new Vector2(-30f, 60f); // Vertical rotation limits

    private float rotationY = 0f; // Vertical rotation
    private float rotationX = 0f; // Horizontal rotation

    void Start()
    {
        // Lock the cursor (optional for testing on PC)
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleTouchInput();
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount == 1) // Single touch for camera control
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                // Get touch delta
                float deltaX = touch.deltaPosition.x;
                float deltaY = touch.deltaPosition.y;

                // Update rotations
                rotationX += deltaX * sensitivity * Time.deltaTime;
                rotationY -= deltaY * sensitivity * Time.deltaTime;

                // Clamp vertical rotation
                rotationY = Mathf.Clamp(rotationY, verticalLimits.x, verticalLimits.y);
            }
        }

        // Apply rotation
        Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);
        Vector3 cameraPosition = rotation * new Vector3(0, 0, -distanceFromPlayer) + player.position;

        // Update camera transform
        cameraTransform.position = cameraPosition;
        cameraTransform.LookAt(player);
    }
}
