using UnityEngine;

public class Tank_Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    public float bulletSpeed = 10f;
    public float shootCooldown = 10f;
    private float tempShoot;

    private void Start()
    {
        tempShoot = shootCooldown;
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
        rb.linearVelocity = firePoint.up * bulletSpeed;  
    }
}
