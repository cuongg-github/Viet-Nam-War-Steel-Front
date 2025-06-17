using UnityEngine;

public class Tank : MonoBehaviour
{

    public Animator trackLeft;
    public Animator trackRight;
    public Rigidbody2D rb;
    public PolygonCollider2D poly;
    public int maxHealth = 100;
    public float moveSpeed = 3f;
    public float rotateSpeed = 100f;

    private int currentHealth;
    Vector2 moveAmount;
    public float doublerotate = 10f;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        rb.angularDamping = 10f;
        rb.linearDamping = 10f;
        rb.gravityScale = 0;
        poly = GetComponent<PolygonCollider2D>();
        float directionX = Mathf.Sign(transform.localScale.x);
    }



    void Update()
    {
        moveAmount = transform.up * Input.GetAxisRaw("Vertical");
    }
    void FixedUpdate()
    {
        if ( Input.GetAxisRaw("Vertical") != 0 )
        {
            rb.MovePosition(rb.position + moveAmount * moveSpeed * Time.fixedDeltaTime);
            if ( Input.GetAxisRaw("Horizontal") != 0 )
            {
                if (Input.GetAxisRaw("Vertical") == 1 )
                {
                    rb.MoveRotation(rb.rotation - Input.GetAxisRaw("Horizontal") * rotateSpeed * Time.fixedDeltaTime);
                } else
                {
                    rb.MoveRotation(rb.rotation + Input.GetAxisRaw("Horizontal") * rotateSpeed * Time.fixedDeltaTime);
                }
            }
            trackStart();
        } else
        {
            trackStop();
        }
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

    void trackStart()
    {
        trackLeft.SetBool("isMoving", true);
        trackRight.SetBool("isMoving", true);
    }

    void trackStop()
    {
        trackLeft.SetBool("isMoving", false);
        trackRight.SetBool("isMoving", false);
    }

}