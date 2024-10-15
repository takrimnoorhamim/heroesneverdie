using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Variables to control movement speed and increment
    public float initialSpeed = 5f;  // Starting speed of the camera
    public float speedIncrement = 1f;  // Speed increase amount
    public float incrementInterval = 2f;  // Time interval for speed increase in seconds
    public float transitionDuration = 0.5f;  // Duration for smooth speed transition

    public float currentSpeed;  // Current movement speed
    private float targetSpeed;  // Target speed to transition to
    private float timer;  // Timer to track time between increments
    private float transitionTimer;  // Timer to track the smooth transition

    void Start()
    {
        // Initialize the current and target speed
        currentSpeed = initialSpeed;
        targetSpeed = initialSpeed;
    }

    void Update()
    {
        // Move the camera to the right based on the current speed
        transform.Translate(Vector3.right * currentSpeed * Time.deltaTime);

        // Update the increment timer
        timer += Time.deltaTime;

        // If the time interval has passed, set the new target speed
        if (timer >= incrementInterval)
        {
            targetSpeed += speedIncrement;  // Set new target speed
            timer = 0f;  // Reset the increment timer
            transitionTimer = 0f;  // Reset the transition timer
        }

        // Smoothly transition the current speed towards the target speed
        if (currentSpeed != targetSpeed)
        {
            transitionTimer += Time.deltaTime;
            currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, transitionTimer / transitionDuration);
        }
    }
}
