// GunBullet.cs
using UnityEngine;

public class GunBullet : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 25f;
    [SerializeField] private float timeDestroy = 5f;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * moveSpeed, ForceMode2D.Impulse); // bắn thẳng

        Destroy(gameObject, timeDestroy);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Gun"))
        {
            GunHealth enemyHealth = collision.GetComponent<GunHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(20); // Gọi phương thức TakeDamage trên đối tượng va chạm
            }
        }
        Destroy(gameObject); // Hủy viên đạn khi va chạm
    }
}