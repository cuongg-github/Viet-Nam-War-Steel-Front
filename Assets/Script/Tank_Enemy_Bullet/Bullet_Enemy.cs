using UnityEngine;

public class Bullet_Enemy : MonoBehaviour
{
    public int damage = 20;
    public float shootRange = 10f;

    private Vector3 startPosition;
    void Start()
    {
        startPosition = transform.position;
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
        if (collision.gameObject.CompareTag("Tank_Ally"))
        {
            Collider2D bulleCollider = GetComponent<Collider2D>();
            Tank tank = collision.gameObject.GetComponent<Tank>();
            if (tank != null)
            {
                tank.TakeDamage(damage);
            }   
        }

        if (collision.gameObject.CompareTag("Tank_AI"))
        {
            Collider2D bulleCollider = GetComponent<Collider2D>();
            TankAIHealth tankai = collision.gameObject.GetComponent<TankAIHealth>();
            if (tankai != null)
            {
                tankai.TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }
}
