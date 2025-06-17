using UnityEngine;

public class TankEnemy : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private bool isDead = false;

    public HealthBar healthBar;
    public Rigidbody2D rb;
    public PolygonCollider2D poly;

    public GameObject bulletReward;
    public GameObject healthReward;

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        rb.angularDamping = 10f;
        rb.linearDamping = 10f;
        rb.gravityScale = 0;
        poly = GetComponent<PolygonCollider2D>();
        float directionX = Mathf.Sign(transform.localScale.x);
        if (healthBar != null)
            healthBar.UpdateBar(currentHealth, maxHealth);
    }

    public void TakeDamage(int damage)
    {

        if (isDead) return;  // Prevent multiple death triggers

        currentHealth -= damage;
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            isDead = true;
            Die();
            SpawnReward();

        }
        healthBar.UpdateBar(currentHealth, maxHealth);
    }

    void Die()
    {
        Destroy(gameObject);
    }
    void SpawnReward()
    {
        int hasBullet = Random.Range(0, 2);
        int hasHealth = Random.Range(0, 2);
        Debug.Log(hasBullet + " " + hasHealth);
        if (hasBullet == 1)
        {
            Instantiate(bulletReward, transform.position, Quaternion.identity);
        }
        if (hasHealth == 1)
        {
            Vector3 offset = new Vector3(3f, 0, 0);
            Instantiate(healthReward, transform.position + offset, Quaternion.identity);
        }

    }
}
