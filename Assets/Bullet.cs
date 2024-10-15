using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AcquiredStudent"))
        {
            StudentRally rally = FindObjectOfType<StudentRally>();
            rally.DeactivateStudent(collision.transform);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Student") || collision.CompareTag("Leader"))
        {
            return;
        }
        Destroy(gameObject);
    }
}