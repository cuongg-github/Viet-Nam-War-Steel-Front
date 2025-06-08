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
        poly.offset = new Vector2((Mathf.Abs(poly.offset.x) * directionX) - 6, poly.offset.y);
        Vector2[] customShape = new Vector2[] {
            new Vector2(2.079555f, 1.121828f),
            new Vector2(2.084945f, 2.416713f),
            new Vector2(2.177241f, 2.641616f),
            new Vector2(2.136999f, 3.520272f),
            new Vector2(2.242123f, 3.505259f),
            new Vector2(2.241472f, 3.811502f),
            new Vector2(1.233817f, 3.807289f),
            new Vector2(1.260905f, 3.480432f),
            new Vector2(1.349822f, 3.465398f),
            new Vector2(1.336686f, 2.521489f),
            new Vector2(1.455752f, 2.470687f),
            new Vector2(1.462505f, 1.384384f),
            new Vector2(1.432197f, 1.118f),
            new Vector2(0.9788589f, 1.096849f),
            new Vector2(0.8317366f, 0.6696522f),
            new Vector2(-0.855773f, 0.6901507f),
            new Vector2(-0.8858914f, -3.627474f),
            new Vector2(4.586815f, -3.563155f),
            new Vector2(4.539133f, 0.6431882f),
            new Vector2(2.831311f, 0.6781554f),
            new Vector2(2.448101f, 0.894232f),
            new Vector2(2.518255f, 1.118867f),
            new Vector2(2.110801f, 1.120951f),
            new Vector2(0.9510565f, 0.3090171f),
        };
        poly.SetPath(0, customShape);

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