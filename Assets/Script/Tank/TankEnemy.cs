using UnityEngine;

public class TankEnemy : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private bool isDead = false;

    public Rigidbody2D rb;
    public BoxCollider2D bc;

    public GameObject bulletReward;
    public GameObject healthReward;

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

        if (isDead) return;  // Prevent multiple death triggers

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            isDead = true;      
            Die();
            SpawnReward();
            
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
    void SpawnReward()
    {
        int hasBullet = Random.Range(0,2);
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
