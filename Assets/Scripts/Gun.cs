using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{

    [SerializeField] private Transform firePos;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float shotDelay = 0.15f;
    [SerializeField] private float bulletSpeed = 25f;

    [SerializeField] private float recoilDistance = 0.1f;
    [SerializeField] private float recoilSpeed = 5f;

    [SerializeField] private GameObject flashEffect;
    [SerializeField] private float flashDuration =1f;
    private Vector3 originalPosition;

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
    }
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePos.position, firePos.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        bullet.transform.localScale = Vector3.one;

        if (rb != null)
        {
            rb.linearVelocity = firePos.right * bulletSpeed;
        }

        StartCoroutine(Recoil());
        StartCoroutine(FlashEffect());
    }

    IEnumerator Recoil()
    {
        Vector3 recoilPos = originalPosition - transform.right * recoilDistance;

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

    IEnumerator FlashEffect()
    {
        SpriteRenderer sr = flashEffect.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.enabled = true;
            yield return new WaitForSeconds(flashDuration);
            sr.enabled = false;
        }
    }

}