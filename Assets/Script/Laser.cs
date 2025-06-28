using UnityEngine;

public class Laser : MonoBehaviour
{
    public int laserDamage = 50;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tank_Enemy"))
        {
            Collider2D bulleCollider = GetComponent<Collider2D>();
            TankEnemy enemy = collision.gameObject.GetComponent<TankEnemy>();
            if (enemy != null)
            {
                Collider2D enemyCollider = enemy.GetComponent<Collider2D>();
                enemy.TakeDamage(laserDamage);
            }
        }
    }
}
