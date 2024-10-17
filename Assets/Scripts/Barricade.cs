using UnityEngine;
using UnityEngine.SceneManagement;

public class Barricade : MonoBehaviour
{
    private StudentRally studentRally;
    public int barricatePower;
    public int levelNumber;

    [SerializeField] private GameObject destructionEffectPrefab;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip destroySound;

    private AudioSource audioSource;

    void Start()
    {
        // Reference the StudentRally component to get the acquired student count
        studentRally = FindObjectOfType<StudentRally>();

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Check for collision
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Only destroy if the collider is tagged as "AcquiredStudent" and the count is barricatePower
        if (collision.gameObject.CompareTag("AcquiredStudent") && studentRally.GetAcquiredStudentCount() >= barricatePower)
        {
            DestroyBarricade();
        }
        else
        {
            if (hitSound != null)
            {
                audioSource.PlayOneShot(hitSound);
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        } 

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Leader") && studentRally.GetAcquiredStudentCount() == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void DestroyBarricade()
    {
        if (destructionEffectPrefab != null)
        {
            Instantiate(destructionEffectPrefab, transform.position, Quaternion.identity);
        }

        if (destroySound != null)
        {
            AudioSource.PlayClipAtPoint(destroySound, transform.position);
        }

        // Unlock the next level
        PlayerPrefs.SetInt($"Level{levelNumber + 1}Unlocked", 1);
        PlayerPrefs.Save();

        // Destroy the barricade
        Destroy(gameObject);
    }
}