using Pathfinding;
using System.Collections;
using UnityEngine;

public class EnemyTankController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float rotationSpeed = 100f;
    public GameObject bulletPrefab;  
    public Transform firePoint;      
    public bool canShoot = false;
    public Transform attackTargetPoint;
    public float stopRange = 20f;
    public float shootCooldown = 5f;
    private Path path;
    private int currentWaypoint = 0;
    private bool canMove = false;
    private Rigidbody2D rb;
    private Seeker seeker;
    private Transform player;
    private float tempShootCooldown;
    private bool isTriggered = false;  
    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Tank_Ally");
        tempShootCooldown = shootCooldown;
        rb = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();
        if (seeker == null)
            Debug.LogError("Seeker chưa được gắn vào " + gameObject.name);
        if (rb == null)
            Debug.LogError("Rigidbody2D chưa được gắn vào " + gameObject.name);
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    public void SetTrigger(bool value)
    {
               isTriggered = value;
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
        if (isTriggered)
        {
            RaycastHit2D shootPoint = Physics2D.Raycast(firePoint.position, firePoint.up, stopRange, LayerMask.GetMask("Tank"));
            float distance = Vector2.Distance(player.GetComponent<Collider2D>().bounds.center, transform.GetComponent<Collider2D>().bounds.center);
            // Debug.Log("Khoảng cách đến Player: " + distance);

            if (distance > stopRange)
            {
                moveFollowPlayer();
            }
            else
            {
                if (shootPoint.collider != null && tempShootCooldown <= 0)  // Kiểm tra cooldown trước khi bắn
                {
                    Shoot();  // Bắn khi đủ điều kiện
                    tempShootCooldown = shootCooldown;  // Reset cooldown
                }
                else
                {
                    tempShootCooldown -= Time.deltaTime;  // Giảm cooldown mỗi frame
                }
            }
        }

        // Kiểm tra nếu kẻ địch có thể di chuyển và có path tính toán
        if (!canMove || path == null || path.vectorPath == null || currentWaypoint >= path.vectorPath.Count)
            return;

        // Tính toán hướng di chuyển đến waypoint tiếp theo
        Vector2 targetPos = path.vectorPath[currentWaypoint];
        Vector2 direction = (targetPos - rb.position).normalized;  // Tính hướng di chuyển

        // Tính toán góc quay để di chuyển về phía mục tiêu
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

        // Di chuyển đến waypoint tiếp theo khi gần
        if (Vector2.Distance(rb.position, targetPos) < 0.2f)
        {
            currentWaypoint++;  // Chuyển sang waypoint tiếp theo khi đã đến gần
        }
    }

    void moveFollowPlayer()
    {
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
            // Tạo viên đạn từ prefab tại firePoint
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            // Lấy Rigidbody2D của viên đạn
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

            if (bulletRb != null)
            {
                // Tạm thời vô hiệu hóa collider để tránh va chạm ngay lập tức
                Collider2D bulletCollider = bullet.GetComponent<Collider2D>();
                if (bulletCollider != null)
                {
                    bulletCollider.enabled = false; // Tắt collider tạm thời
                }

                bulletRb.linearVelocity = firePoint.up * 10f; // Thiết lập vận tốc cho viên đạn

                StartCoroutine(EnableColliderAfterDelay(bulletCollider, 0.1f));  // Bật collider sau 0.1s
            }
        }
        else
        {
            Debug.LogWarning("Bullet prefab hoặc fire point không được gán trong EnemyTankController.");
        }
    }

    // Hàm để bật lại collider sau một khoảng thời gian ngắn
    private IEnumerator EnableColliderAfterDelay(Collider2D bulletCollider, float delay)
    {
        yield return new WaitForSeconds(delay);  // Chờ một khoảng thời gian
        if (bulletCollider != null)
        {
            bulletCollider.enabled = true;  // Bật lại collider sau khi viên đạn di chuyển một chút
        }
    }




}
