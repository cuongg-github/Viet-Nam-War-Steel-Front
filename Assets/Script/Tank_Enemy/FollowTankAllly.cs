using System;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.GraphicsBuffer;

public class FollowTankAllly : MonoBehaviour
{
    // khoi dong xe tang
    //  rb.MoveRotation(-rb.rotation + 1 * 50f * Time.fixedDeltaTime);
    public float detectRange = 30f;
    public float shootRange = 20f;
    public float stopRange = 20f;
    public float moveSpeed = 2f;
    public float rotateSpeed = 50f;
    public int shootCooldown = 10;
    private int hideShootCooldown;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform heavyHull;
    public Rigidbody2D rb;
    private Transform player;
    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Tank_Ally");
        rb = GetComponent<Rigidbody2D>();
        hideShootCooldown = shootCooldown;
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void FixedUpdate()
    {
        //Vector2 direct = player.position - transform.position;
        float distance = Vector2.Distance(player.GetComponent<Collider2D>().bounds.center, transform.GetComponent<Collider2D>().bounds.center);
        RaycastHit2D shootPoint = Physics2D.Raycast(firePoint.position, firePoint.up, shootRange, LayerMask.GetMask("Tank"));
        RaycastHit2D movePoint = Physics2D.Raycast(heavyHull.position, heavyHull.up, detectRange, LayerMask.GetMask("Tank"));
        if (distance < detectRange)
        {
            //Debug.Log($"player position: {player.position} enemy position: {transform.position} distance: {distance} and stoprange: {stopRange}");
            if (distance > stopRange)
            {
                Debug.DrawRay(heavyHull.position, heavyHull.up * detectRange, Color.green);
                if (movePoint.collider != null)
                {
                    rb.MovePosition(rb.position + (Vector2)transform.up * moveSpeed * Time.fixedDeltaTime);
                }
                else
                {
                    moveFollowPlayer();
                }
            }
            else
            {
                if (shootPoint.collider != null)
                {
                    if ( shootCooldown == 0 )
                    {
                        Shoot();
                        shootCooldown = hideShootCooldown;
                    } else
                    {
                        shootCooldown -= 1;
                    }
                } else
                {
                    moveFollowPlayer();
                }
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
                    rb.linearVelocity = firePoint.up * 8f;
                }
            }
        }

        void moveFollowPlayer()
        {
            if (player.position.x > transform.position.x && player.position.y > transform.position.y)
            {
                Debug.Log("right up");
                rb.MoveRotation(rb.rotation + rotateSpeed * -1 * 10f * Time.fixedDeltaTime);
            }
            if (player.position.x < transform.position.x && player.position.y > transform.position.y)
            {
                Debug.Log("left up");
                rb.MoveRotation(rb.rotation + rotateSpeed * 10f * Time.fixedDeltaTime);
            }
            if (player.position.x < transform.position.x && player.position.y < transform.position.y)
            {
                Debug.Log("left down");
                rb.MoveRotation(rb.rotation + rotateSpeed * 10f * Time.fixedDeltaTime);
            }
            if (player.position.x > transform.position.x && player.position.y < transform.position.y)
            {
                Debug.Log("right down");
                rb.MoveRotation(rb.rotation + rotateSpeed * -1 * 10f * Time.fixedDeltaTime);
            }
        }
    }
}
