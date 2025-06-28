using System;
using UnityEngine;
public class Bullet_M1 : MonoBehaviour
{
    public int damage = 20;
    public float shootRange = 20f;

    private Vector3 startPosition;
    void Start()
    {
        startPosition = transform.position;
        GameObject area = GameObject.FindWithTag("Area");
        GameObject map = GameObject.FindWithTag("Map1"); 
        if (map != null && area != null)
        {
            Collider2D areaColider = area.GetComponent<Collider2D>();
            Collider2D mapCollider = map.GetComponent<Collider2D>();
            Collider2D bulletCollider = GetComponent<Collider2D>();
            if (mapCollider != null && bulletCollider != null)
            {
                Physics2D.IgnoreCollision(bulletCollider, mapCollider);
                Physics2D.IgnoreCollision(bulletCollider, areaColider);
            }
        }
    }

    void Update()
    {
        if (Vector3.Distance(startPosition, transform.position) >= shootRange)
        {
            Destroy(gameObject); 
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tank_Enemy"))
        {
            Collider2D bulleCollider = GetComponent<Collider2D>();
            TankEnemy enemy = collision.gameObject.GetComponent<TankEnemy>();
            if (enemy != null)
            {
                Collider2D enemyCollider = enemy.GetComponent<Collider2D>();
                enemy.TakeDamage(damage);
            }
        }
        if (collision.gameObject.CompareTag("Gun"))
        {
            GunHealth enemyHealth = collision.gameObject.GetComponent<GunHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }
        Destroy(gameObject); 
    }

}
