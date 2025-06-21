using UnityEngine;

public class TankAI : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 3f; 
    public float followDistance = 3f;
    public GameObject player; 
    public PolygonCollider2D poly;

    private Vector2 moveAmount;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.angularDamping = 10f;
        rb.linearDamping = 10f;
        rb.gravityScale = 0;
        poly = GetComponent<PolygonCollider2D>();
        player = GameObject.FindGameObjectWithTag("Tank_Ally");
    }



    void Update()
    {
        if (player != null)
        {
            FollowPlayer();
        }
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            FollowPlayerMovement();
        }
    }

    void FollowPlayer()
    {
        // Tính toán khoảng cách giữa xe tăng đồng đội và player
        float distance = Vector2.Distance(transform.position, player.transform.position);

        // Nếu xe tăng đồng đội cách player quá xa, nó sẽ di chuyển về phía player
        if (distance > followDistance)
        {
            moveAmount = (player.transform.position - transform.position).normalized;  // Di chuyển hướng về player
            MoveTank();
        }
        else
        {
            StopMoving();  // Nếu xe tăng đồng đội đã đủ gần, dừng di chuyển
        }
    }

    void FollowPlayerMovement()
    {
        rb.linearVelocity = new Vector2(moveAmount.x * moveSpeed, moveAmount.y * moveSpeed);
    }

    void MoveTank()
    {
        rb.linearVelocity = new Vector2(moveAmount.x * moveSpeed, moveAmount.y * moveSpeed); // Di chuyển tank đồng đội
    }
    void StopMoving()
    {
        // Dừng di chuyển khi đủ gần player
        rb.linearVelocity = Vector2.zero;
    }
}