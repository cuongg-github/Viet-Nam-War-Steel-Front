using UnityEngine;

public class TankEnemy : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public Rigidbody2D rb;
    public BoxCollider2D bc;

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        rb.angularDamping = 10f;
        rb.gravityScale = 0;
        Vector2 size = bc.size;
        rb.bodyType = RigidbodyType2D.Kinematic;
        float directionX = Mathf.Sign(transform.localScale.x);
        bc.offset = new Vector2((Mathf.Abs(bc.offset.x) * directionX) - 3, bc.offset.y);
        bc.size = new Vector2(2.5f, 4);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
