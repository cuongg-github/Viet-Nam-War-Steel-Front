using UnityEngine;

public class Tank_Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    public float bulletSpeed = 10f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = firePoint.up * bulletSpeed;  
    }
}
