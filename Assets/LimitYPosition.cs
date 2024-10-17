using UnityEngine;

public class LimitYPosition : MonoBehaviour
{
    // The maximum Y value the object can reach, set in the Inspector
    public float maxY;

    void Update()
    {
        // Get the current position of the object
        Vector3 currentPosition = transform.position;

        // If the object's Y position is greater than maxY, set it to maxY
        if (currentPosition.y > maxY)
        {
            currentPosition.y = maxY;
            transform.position = currentPosition;
        }
    }
}
