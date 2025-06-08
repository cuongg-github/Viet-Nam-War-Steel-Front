using UnityEngine;

public class Bullet_Enemy : MonoBehaviour
{
    public int damage = 20;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tank_Ally"))
        {
            Collider2D bulleCollider = GetComponent<Collider2D>();
            Tank tank = collision.gameObject.GetComponent<Tank>();
            if (tank != null)
            {
                tank.TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }
}
