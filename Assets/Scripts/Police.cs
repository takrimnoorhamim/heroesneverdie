using UnityEngine;

public class Police : MonoBehaviour
{
    public GameObject bulletPrefab;  // Bullet to be shot
    public Transform[] spawnPoints;  // Array of possible spawn points
    public float fireRate = 2f;  // Time between shots
    private float fireTimer = 0f;

    public AudioClip shootClip;  // Reference to the audio clip to be played
    public AudioSource audioSource;  // Reference to the AudioSource

    void Start()
    {
        
    }

    void Update()
    {
        // Shoot bullets at intervals
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireRate)
        {
            Shoot();
            fireTimer = 0f;
        }
    }

    void Shoot()
    {
        // Choose a random spawn point
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform randomSpawnPoint = spawnPoints[randomIndex];

        // Instantiate a bullet at the random spawn point's position and rotation
        Instantiate(bulletPrefab, randomSpawnPoint.position, randomSpawnPoint.rotation);

        // Play the audio clip
        if (audioSource != null && shootClip != null)
        {
            audioSource.PlayOneShot(shootClip);
        }
    }
}
