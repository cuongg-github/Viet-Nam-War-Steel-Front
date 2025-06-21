using UnityEngine;

public class Bullet_EMap1 : MonoBehaviour
{
    public int damage = 20; // Sát thương viên đạn
    public float shootRange = 10f; // Tầm bắn viên đạn

    private Vector3 startPosition; // Lưu trữ vị trí xuất phát của viên đạn

    void Start()
    {
        startPosition = transform.position;  // Ghi lại vị trí xuất phát
    }

    void Update()
    {
        // Kiểm tra nếu viên đạn đã đi ra ngoài phạm vi shootRange, sẽ hủy viên đạn
        if (Vector3.Distance(startPosition, transform.position) >= shootRange)
        {
            Destroy(gameObject);  // Hủy viên đạn nếu vượt quá tầm bắn
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra nếu viên đạn va chạm với đối tượng có tag "Tank_Ally"
        if (collision.gameObject.CompareTag("Tank_Ally"))
        {
            // Lấy đối tượng Tank từ gameObject va chạm
            Tank tank = collision.gameObject.GetComponent<Tank>();
            if (tank != null)
            {
                tank.TakeDamage(damage);  // Gọi hàm TakeDamage để gây sát thương
            }
        }

        // Sau khi xử lý va chạm, hủy viên đạn
        Destroy(gameObject);  // Hủy viên đạn sau khi va chạm
    }
}
