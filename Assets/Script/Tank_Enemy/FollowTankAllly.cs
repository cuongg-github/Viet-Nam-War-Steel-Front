using System;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.GraphicsBuffer;

public class FollowTankAllly : MonoBehaviour
{
    // khoi dong xe tang
    //  rb.MoveRotation(-rb.rotation + 1 * 50f * Time.fixedDeltaTime);
    public float detectRange = 30f;
    public float stopRange = 20f;
    public float moveSpeed = 2f;
    public float rotateSpeed = 50f;
    public int shootCooldown = 10;
    public float bulletSpeed = 50f;
    private int hideShootCooldown;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform heavyHull;
    public Rigidbody2D rb;
    private Transform player;
    public AudioClip movementSound;
    private AudioSource audioSource;
    public AudioClip shootSound;
    public GameObject shootEffect;

    public Animator trackLeft;
    public Animator trackRight;
    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Tank_Ally");
        rb = GetComponent<Rigidbody2D>();
        hideShootCooldown = shootCooldown;
        audioSource = GetComponent<AudioSource>();
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void FixedUpdate()
    {
        //Vector2 direct = player.position - transform.position;
        float distance = Vector2.Distance(player.GetComponent<Collider2D>().bounds.center, transform.GetComponent<Collider2D>().bounds.center);
        RaycastHit2D shootPoint = Physics2D.Raycast(firePoint.position, firePoint.up, stopRange, LayerMask.GetMask("Tank"));
        RaycastHit2D movePoint = Physics2D.Raycast(heavyHull.position, heavyHull.up, detectRange, LayerMask.GetMask("Tank"));
        if (distance < detectRange)
        {
            //Debug.Log($"player position: {player.position} enemy position: {transform.position} distance: {distance} and stoprange: {stopRange}");
            if (distance > stopRange)
            {
                //Debug.DrawRay(heavyHull.position, heavyHull.up * detectRange, Color.green);
                if (movePoint.collider != null)
                {
                    Debug.Log("hit");
                    rb.MovePosition(rb.position + (Vector2)transform.up * moveSpeed * Time.fixedDeltaTime);
                }
                else
                {
                    Debug.Log("Not hit");
                    moveFollowPlayer();
                }
                trackStart();
            }
            else
            {
                if (shootPoint.collider != null)
                {
                    trackStop();
                    if (shootCooldown == 0)
                    {
                        Shoot();
                        shootCooldown = hideShootCooldown;
                    }
                    else
                    {
                        shootCooldown -= 1;
                    }
                }
                else
                {
                    moveFollowPlayer();
                    trackStart();
                }
            }
        } else
        {
            trackStop();
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Quaternion customRotation = firePoint.rotation * Quaternion.Euler(0, 0, -90);
                Instantiate(shootEffect, firePoint.position, customRotation);
                rb.linearVelocity = firePoint.up * bulletSpeed;
            }
            audioSource.PlayOneShot(shootSound);
        }
    }

    void moveFollowPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        float angleDiff = Mathf.DeltaAngle(rb.rotation, angle);

        float rotateStep = rotateSpeed * Time.fixedDeltaTime;

        if (Mathf.Abs(angleDiff) < rotateStep)
        {
            rb.MoveRotation(angle);
        }
        else
        {
            rb.MoveRotation(rb.rotation + Mathf.Sign(angleDiff) * rotateStep);
        }
    }

    void trackStart()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = movementSound;
            audioSource.loop = true;
            audioSource.Play();
        }

        trackLeft.SetBool("isMoving", true);
        trackRight.SetBool("isMoving", true);
    }

    void trackStop()
    {
        if (audioSource.clip == movementSound)
        {
            audioSource.Stop();
            audioSource.clip = null;
        }

        trackLeft.SetBool("isMoving", false);
        trackRight.SetBool("isMoving", false);
    }
}
