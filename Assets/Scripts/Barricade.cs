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
    public AudioSource audioSource;

    void Start()
    {
        studentRally = FindObjectOfType<StudentRally>();

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
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
        // Create a new GameObject to handle the destruction effects
        GameObject destructionHandler = new GameObject("DestructionHandler");
        DestructionHandler handler = destructionHandler.AddComponent<DestructionHandler>();

        // Pass necessary information to the handler
        handler.Initialize(destructionEffectPrefab, destroySound, transform.position, levelNumber);

        // Destroy the barricade
        Destroy(gameObject);
    }
}

public class DestructionHandler : MonoBehaviour
{
    private GameObject effectPrefab;
    private AudioClip destroySound;
    private Vector3 position;
    private int levelNumber;

    public void Initialize(GameObject effectPrefab, AudioClip destroySound, Vector3 position, int levelNumber)
    {
        this.effectPrefab = effectPrefab;
        this.destroySound = destroySound;
        this.position = position;
        this.levelNumber = levelNumber;

        // Start the destruction sequence
        StartCoroutine(DestructionSequence());
    }

    private System.Collections.IEnumerator DestructionSequence()
    {
        // Play the explosion effect
        if (effectPrefab != null)
        {
            Instantiate(effectPrefab, position, Quaternion.identity);
        }

        // Play the destroy sound
        if (destroySound != null)
        {
            AudioSource.PlayClipAtPoint(destroySound, position);
        }

        // Unlock the next level
        PlayerPrefs.SetInt($"Level{levelNumber + 1}Unlocked", 1);
        PlayerPrefs.Save();

        // Wait for the sound to finish playing
        if (destroySound != null)
        {
            yield return new WaitForSeconds(destroySound.length);
        }

        // Destroy this handler object
        Destroy(gameObject);
    }
}