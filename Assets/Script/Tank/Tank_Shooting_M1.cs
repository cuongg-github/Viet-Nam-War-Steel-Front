using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Tank_Shooting_M1 : MonoBehaviour
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
            }
            else
            {
                shootCooldown -= 1;
            }
        }

    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            // Tạo viên đạn từ prefab tại firePoint
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            // Lấy Rigidbody2D của viên đạn
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            Quaternion customRotation = firePoint.rotation * Quaternion.Euler(0, 0, -90);
            Instantiate(shootEffect, firePoint.position, customRotation);
            if (bulletRb != null)
            {
                bulletRb.linearVelocity = firePoint.up * 10f; 
            }
        }
        else
        {
            Debug.LogWarning("Bullet prefab hoặc fire point không được gán trong EnemyTankController.");
        }
    }

}
