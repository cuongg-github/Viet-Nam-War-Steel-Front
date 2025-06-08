using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{

    [SerializeField] private Transform firePos;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float shotDelay = 0.15f;
    [SerializeField] private float bulletSpeed = 25f;

    [SerializeField] private float recoilDistance = 0.1f;
    [SerializeField] private float recoilSpeed = 20f;
    [SerializeField] private Transform turretToRotate;

    private Vector3 originalPosition;
    [SerializeField] private Transform playerTransform;

    private float nextShotTime;
    void Start()
    {
        originalPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextShotTime)
        {
            Shoot();
            nextShotTime = Time.time + shotDelay;
        }

        if (playerTransform != null && turretToRotate != null)
        {
            Vector3 direction = playerTransform.position - turretToRotate.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            turretToRotate.rotation = Quaternion.Euler(0f, 0f, angle);
        }

    }
    void Shoot()
    {
        if (firePos == null || bulletPrefab == null) return;

        GameObject bullet = Instantiate(bulletPrefab, firePos.position, firePos.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity = firePos.right * bulletSpeed;
        }

        StartCoroutine(Recoil());
    }

    IEnumerator Recoil()
    {
        Vector3 recoilDir = -firePos.right; // giật ngược hướng bắn
        Vector3 recoilPos = originalPosition + recoilDir * recoilDistance;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * recoilSpeed;
            transform.localPosition = Vector3.Lerp(originalPosition, recoilPos, t);
            yield return null;
        }

        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * recoilSpeed;
            transform.localPosition = Vector3.Lerp(recoilPos, originalPosition, t);
            yield return null;
        }
    }
}