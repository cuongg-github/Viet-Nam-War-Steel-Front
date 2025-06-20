using UnityEngine;

public class Tank_Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public AudioClip shootSound;
    public GameObject shootEffect;
    private AudioSource AudioSource;


    public float bulletSpeed = 10f;
    public float shootCooldown = 10f;
    private float tempShoot;
    private void Start()
    {
        tempShoot = shootCooldown;
        AudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (shootCooldown == 0) 
            {
                Shoot();
                shootCooldown += tempShoot;
            } else
            {
                shootCooldown -= 1;
            }
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Quaternion customRotation = firePoint.rotation * Quaternion.Euler(0, 0, -90);
        Instantiate(shootEffect, firePoint.position, customRotation);
        rb.linearVelocity = firePoint.up * bulletSpeed;
        AudioSource.PlayOneShot(shootSound);
    }
}
