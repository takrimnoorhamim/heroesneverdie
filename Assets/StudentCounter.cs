using UnityEngine;
using UnityEngine.UI;  // Required for UI components

public class StudentCounter : MonoBehaviour
{
    public Text studentCountText;  // Reference to the UI Text component
    private StudentRally studentRally;  // Reference to the StudentRally script

    void Start()
    {
        // Get the StudentRally component from the scene
        studentRally = FindObjectOfType<StudentRally>();
    }

    void Update()
    {
        // Update the text with the current number of acquired students
        int count = studentRally.GetAcquiredStudentCount();
        studentCountText.text = "Acquired Students: " + count;
    }
}
