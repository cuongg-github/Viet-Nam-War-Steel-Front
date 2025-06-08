using UnityEngine;
public class Bullet : MonoBehaviour
{
    public int damage = 20;

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

        Destroy(gameObject); 
    }
}
