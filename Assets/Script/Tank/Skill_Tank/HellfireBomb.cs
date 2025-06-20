using UnityEngine;

public class HellfireBomb : MonoBehaviour
{
    public float explosionRadius = 20f;
    public int explosionDamage = 50;
    public LayerMask damageLayer;
    public GameObject explosionEffectPrefab;

    public AudioClip exlosionSound;
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Invoke("Explode", 0.3f); 
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Explode();
    }
    void Explode()
    {
        GameObject explosion = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, 0.5f);
        if (exlosionSound != null)
        {
            AudioSource.PlayClipAtPoint(exlosionSound, transform.position);
        }
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, explosionRadius, damageLayer);
        foreach (Collider2D hit in hitObjects)
        {
            if (hit.CompareTag("Tank_Enemy"))
            {
                TankEnemy enemy = hit.GetComponent<TankEnemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(explosionDamage);
                }
            }

            if (hit.CompareTag("Gun"))
            {
                GunHealth gun = hit.GetComponent<GunHealth>();
                if (gun != null)
                {
                    gun.TakeDamage(explosionDamage);
                }
            }
        }

        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
