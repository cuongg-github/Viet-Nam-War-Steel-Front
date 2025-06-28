using UnityEngine;

public class BulletE_M1 : MonoBehaviour
{
    public int damage = 20; // Sát thương viên đạn
    public float shootRange = 20f; // Tầm bắn viên đạn

    private Vector3 startPosition; // Lưu trữ vị trí xuất phát của viên đạn

    void Start()
    {
        startPosition = transform.position;

        // Bỏ qua va chạm với bản đồ (Map1)
        GameObject[] map = GameObject.FindWithTag("Map1");
        Debug.Log("Map1: " + map);
        if (map == null)
        {
            Debug.LogError("Không tìm thấy đối tượng có tag 'Map1'!");
        }
        if (map != null)
        {
            Collider2D mapCollider = map.GetComponent<Collider2D>();
            Collider2D bulletCollider = GetComponent<Collider2D>();
            if (mapCollider != null && bulletCollider != null)
            {
                Physics2D.IgnoreCollision(bulletCollider, mapCollider);
            }
        }

        // Bỏ qua va chạm với VisionArea
        GameObject enemy = GameObject.FindWithTag("Tank_Enemy"); // hoặc truyền từ prefab cha
        if (enemy != null)
        {
            Transform vision = enemy.transform.Find("VisionArea");
            if (vision != null)
            {
                Collider2D visionCollider = vision.GetComponent<Collider2D>();
                Collider2D bulletCollider = GetComponent<Collider2D>();
                if (visionCollider != null && bulletCollider != null)
                {
                    Physics2D.IgnoreCollision(bulletCollider, visionCollider);
                }
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Đạn va chạm với: " + collision.gameObject.name);
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
