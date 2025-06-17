// GunBullet.cs
using UnityEngine;

public class GunBullet : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 25f;
    [SerializeField] private float timeDestroy = 5f;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tank_Ally"))
        {
            Tank tankally = collision.GetComponent<Tank>();
            if ( tankally != null )
            {
                tankally.TakeDamage(20); 
            }
        }
        Destroy(gameObject);
    }
}