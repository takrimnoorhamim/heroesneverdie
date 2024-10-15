using UnityEngine;
using UnityEngine.SceneManagement;  // For restarting the scene

public class PlayerMovement : MonoBehaviour
{
    public float baseMoveSpeed = 5f;  // Player's base movement speed
    private CameraMove cameraMoveScript;  // Reference to the camera's movement script
    private Camera mainCamera;  // Reference to the main camera

    void Start()
    {
        // Get reference to the main camera and its movement script
        mainCamera = Camera.main;
        cameraMoveScript = mainCamera.GetComponent<CameraMove>();
    }

    void Update()
    {
        // Get input for player movement (WASD)
        float moveX = Input.GetAxis("Horizontal");  // A/D or Left/Right arrow
        float moveY = Input.GetAxis("Vertical");  // W/S or Up/Down arrow

        // Get the current speed of the camera
        float currentCameraSpeed = cameraMoveScript.currentSpeed;

        // Calculate the player's actual movement speed (base speed + camera speed)
        float actualMoveSpeed = baseMoveSpeed + currentCameraSpeed;

        // Move the player, allowing movement in both axes but keeping camera speed in sync on X-axis
        Vector3 movement = new Vector3(moveX * actualMoveSpeed, moveY * baseMoveSpeed, 0f) * Time.deltaTime;
        transform.Translate(movement);

        // Keep the player within the camera bounds
        KeepPlayerInBounds();
    }

    void KeepPlayerInBounds()
    {
        // Get the camera's viewport boundaries
        Vector3 minBounds = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 maxBounds = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));

        // Get the player's current position
        Vector3 playerPos = transform.position;

        // Check if the player has gone out of bounds on the left side
        if (playerPos.x < minBounds.x)
        {
            // Restart the scene if the player exits the left side
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // Restrict movement to stay inside camera's top, bottom, and right bounds
        playerPos.x = Mathf.Clamp(playerPos.x, minBounds.x, maxBounds.x);
        playerPos.y = Mathf.Clamp(playerPos.y, minBounds.y, maxBounds.y);

        // Apply the clamped position to the player
        transform.position = playerPos;
    }


}