using Pathfinding;
using UnityEngine;

public class EnemyTankController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float rotationSpeed = 100f;
    public GameObject bulletPrefab;  
    public Transform firePoint;      
    public bool canShoot = false;
    public Transform attackTargetPoint;
    public float stopRange = 5f;
    private Path path;
    private int currentWaypoint = 0;
    private bool canMove = false;
    private Rigidbody2D rb;
    private Seeker seeker;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();
        if (seeker == null)
            Debug.LogError("Seeker chưa được gắn vào " + gameObject.name);
        if (rb == null)
            Debug.LogError("Rigidbody2D chưa được gắn vào " + gameObject.name);
    }

    public void StartMovingTo(Transform target)
    {
        if (seeker == null)
        {
            seeker = GetComponent<Seeker>();
            if (seeker == null)
            {
                Debug.LogError("[EnemyTankController] Seeker is NULL on object: " + gameObject.name);
                return;
            }
        }

        if (seeker.IsDone())
        {
            seeker.StartPath(transform.position, target.position, OnPathComplete);
        }
    }


    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
            canMove = true;
        }
    }

    void FixedUpdate()
    {
        if (!canMove || path == null || path.vectorPath == null || currentWaypoint >= path.vectorPath.Count)
            return;

        Vector2 targetPos = path.vectorPath[currentWaypoint];
        Vector2 direction = (targetPos - rb.position).normalized;

        // Tính toán góc xoay
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        float currentAngle = rb.rotation;
        float angleDiff = Mathf.DeltaAngle(currentAngle, targetAngle);
        float rotationStep = rotationSpeed * Time.fixedDeltaTime;

        // Xoay về hướng mục tiêu
        if (Mathf.Abs(angleDiff) < rotationStep)
        {
            rb.MoveRotation(targetAngle);
        }
        else
        {
            rb.MoveRotation(currentAngle + Mathf.Sign(angleDiff) * rotationStep);
        }

        // Di chuyển về phía mục tiêu
        Vector2 moveDirection = direction * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + moveDirection);

        // Kiểm tra khoảng cách giữa kẻ địch và AttackTargetPoint (hoặc player)
        
        float distanceToTarget = Vector2.Distance(transform.position, attackTargetPoint.transform.position);

        // Nếu khoảng cách nhỏ hơn stopRange, chuyển sang chế độ moveFollowPlayer và bắn
        if (distanceToTarget < stopRange)
        {
            Debug.Log("Enemy chuyển sang chế độ bắn và theo dõi player.");
            moveFollowPlayer();
            if (canShoot)
            {
                Shoot();
            }
        }
        else
        {
            // Di chuyển đến waypoint tiếp theo khi gần
            if (Vector2.Distance(rb.position, targetPos) < 0.2f)
            {
                currentWaypoint++;
            }
        }
    }

    void moveFollowPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Tank_Ally");
        Vector3 direction = player.transform.position - transform.position;
        float angleToTarget = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        float angleDiff = Mathf.DeltaAngle(rb.rotation, angleToTarget);

        // Xoay về phía mục tiêu
        if (Mathf.Abs(angleDiff) > 1f)
        {
            rb.MoveRotation(rb.rotation + Mathf.Sign(angleDiff) * rotationSpeed * Time.fixedDeltaTime);
        }

        // Di chuyển về phía player
        Vector2 moveDirection = direction.normalized * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + moveDirection);
    }


    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            if (bulletRb != null)
            {
                bulletRb.linearVelocity = firePoint.up * 8f;  // Di chuyển viên đạn
            }
        }
    }



}
