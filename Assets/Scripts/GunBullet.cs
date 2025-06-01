using UnityEngine;

public class GunBullet : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 100f;
    [SerializeField] private float timeDestroy = 20f;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       rb.AddForce(transform.right * moveSpeed, ForceMode2D.Impulse);

        Destroy(gameObject, timeDestroy);
    }

    // Update is called once per frame
    //void Update()
    //{
    //}
}
