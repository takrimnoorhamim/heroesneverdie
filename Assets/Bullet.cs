using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;  // Speed of the bullet
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;  // Move the bullet to the right
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the bullet hits an "AcquiredStudent"
        if (collision.CompareTag("AcquiredStudent"))
        {
            ApplyEffect(2f);  // Apply 1.25x multiplier for acquired student
            Destroy(gameObject);  // Destroy the bullet after the trigger
        }
        // If it hits a "Student" or "Leader", do nothing
        else if (collision.CompareTag("Student") || collision.CompareTag("Leader"))
        {
            // No effect for Students and Leaders
            return;
        }

        // Destroy the bullet after any applicable collision
        Destroy(gameObject);
    }

    void ApplyEffect(float multiplier)
    {
        // Access the student rally script and increase their number accordingly
        StudentRally rally = FindObjectOfType<StudentRally>();
        rally.MultiplyStudents(multiplier);
    }
}
