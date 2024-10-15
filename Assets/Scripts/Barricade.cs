using UnityEngine;
using UnityEngine.SceneManagement;

public class Barricade : MonoBehaviour
{
    private StudentRally studentRally;

    void Start()
    {
        // Reference the StudentRally component to get the acquired student count
        studentRally = FindObjectOfType<StudentRally>();
    }

    // Check for collision
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Only destroy if the collider is tagged as "AcquiredStudent" and the count is 30
        if (collision.gameObject.CompareTag("AcquiredStudent") && studentRally.GetAcquiredStudentCount() >= 30)
        {
            Destroy(this.gameObject); // Destroy the barricade
        }      
        else
        {
            SceneManager.LoadScene(0);
        }
    }
}
