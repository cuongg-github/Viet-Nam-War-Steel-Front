using UnityEngine;
public class Bullet : MonoBehaviour
{
    public int damage = 20;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tank_Enemy"))
        {
            TankEnemy enemy = collision.gameObject.GetComponent<TankEnemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }

        Destroy(gameObject); 
    }
}
