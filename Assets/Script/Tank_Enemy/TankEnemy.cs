using UnityEngine;

public class TankEnemy : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private bool isDead = false;

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
        //poly.offset = new Vector2((Mathf.Abs(poly.offset.x) * directionX) - 14, poly.offset.y);
        //Vector2[] customShape = new Vector2[] {
        //     new Vector2(1.24575f, 2.842229f),
        //     new Vector2(1.220404f, 2.495196f),
        //     new Vector2(1.353826f, 2.450092f),
        //     new Vector2(1.447513f, 1.430279f),
        //     new Vector2(0.8301296f, 0.3767039f),
        //     new Vector2(-0.6718264f, 0.2750994f),
        //     new Vector2(-0.8468838f, 0.0813577f),
        //     new Vector2(-0.894825f, -3.79548f),
        //     new Vector2(1.748753f, -4.093091f),
        //     new Vector2(4.39814f, -3.780972f),
        //     new Vector2(4.353048f, 0.2023317f),
        //     new Vector2(2.642798f, 0.2968785f),
        //     new Vector2(0.5877857f, -0.8090169f),
        //     new Vector2(2.109449f, -0.1612493f),
        //     new Vector2(2.07116f, 1.030087f),
        //     new Vector2(2.188993f, 1.555409f),
        //     new Vector2(2.205173f, 2.489117f),
        //     new Vector2(2.227374f, 2.872541f),
        //};
        //poly.SetPath(0, customShape);
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
