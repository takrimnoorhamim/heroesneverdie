using UnityEngine;

public class Police : MonoBehaviour
{
    public GameObject bulletPrefab;  // Bullet to be shot
    public Transform shootPoint;  // Where the bullet is fired from
    public float fireRate = 2f;  // Time between shots
    private float fireTimer = 0f;

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
        // Instantiate a bullet at the shootPoint position and direction
        Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
    }
}
