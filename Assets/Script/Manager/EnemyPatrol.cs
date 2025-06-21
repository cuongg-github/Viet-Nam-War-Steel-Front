using Pathfinding;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    public Transform pointA;
    public Transform pointB;
    private Path path;
    private int currentWaypoint = 0;
    private bool canMove = false;
    private Rigidbody2D rb;
    private Seeker seeker;
    private Transform targetPoint;  // Lưu điểm mục tiêu hiện tại

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();

        if (seeker == null)
            Debug.LogError("Seeker chưa được gắn vào " + gameObject.name);
        if (rb == null)
            Debug.LogError("Rigidbody2D chưa được gắn vào " + gameObject.name);

        targetPoint = pointA;  // Bắt đầu với điểm tuần tra A
        StartMovingTo(targetPoint);  // Tính toán đường đi đến pointA
    }

    public void StartMovingTo(Transform target)
    {
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
            canMove = true;  // Đảm bảo rằng canMove được đặt là true sau khi có path
            Debug.Log("Path calculated with " + path.vectorPath.Count + " waypoints.");
        }
        else
        {
            Debug.LogError("Path calculation failed: " + p.errorLog);
        }
    }

    void FixedUpdate()
    {
        if (!canMove || path == null || path.vectorPath == null || path.vectorPath.Count == 0)
        {
            Debug.Log("Path is not ready or has no waypoints.");
            return;
        }

        MoveToWaypoint();  // Gọi hàm di chuyển đến waypoint
    }

    // Hàm di chuyển tới waypoint
    void MoveToWaypoint()
    {
        Vector2 targetPos = path.vectorPath[currentWaypoint];
        Vector2 direction = (targetPos - rb.position).normalized;  // Tính hướng di chuyển

        // Tính toán góc quay để di chuyển về phía mục tiêu
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;  // Tính góc quay
        float currentAngle = rb.rotation;
        float angleDiff = Mathf.DeltaAngle(currentAngle, targetAngle);  // Tính sự khác biệt góc

        // Xoay về hướng mục tiêu (dùng Lerp để xoay mượt mà)
        float rotationStep = rotationSpeed * Time.fixedDeltaTime;
        if (Mathf.Abs(angleDiff) > rotationStep)
        {
            rb.MoveRotation(currentAngle + Mathf.Sign(angleDiff) * rotationStep);  // Xoay đối tượng
        }
        else
        {
            rb.MoveRotation(targetAngle);  // Đảm bảo quay về hướng mục tiêu khi đủ gần
        }

        // Di chuyển về phía mục tiêu
        Vector2 moveDirection = direction * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + moveDirection);

        // Di chuyển đến waypoint tiếp theo khi gần
        if (Vector2.Distance(rb.position, targetPos) < 0.5f)  // Kiểm tra gần hơn
        {
            currentWaypoint++;
            
            // Kiểm tra nếu đã đến cuối path, chuyển mục tiêu sang điểm tiếp theo
            if (currentWaypoint >= path.vectorPath.Count)
            {
                // Chuyển mục tiêu sang điểm còn lại (A <-> B)
                targetPoint = (targetPoint == pointA) ? pointB : pointA;
                StartMovingTo(targetPoint);  // Tính toán lại đường đi đến điểm tiếp theo
            }
        }
    }
}
