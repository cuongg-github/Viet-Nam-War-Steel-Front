using TMPro;
using UnityEngine;
public class Tank_Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    public float bulletSpeed = 10f;
    public float shootCooldown = 10f;
    private float tempShoot;

    public GameObject hellfireBombPrefab;
    public TextMeshProUGUI hellfireCooldownText;

    public float bombCooldown = 15f;
    private float nextBombTime = 0f;


    private void Start()
    {
        tempShoot = shootCooldown;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (shootCooldown == 0) 
            {
                Shoot();
                shootCooldown += tempShoot;
            } else
            {
                shootCooldown -= 1;
            }
        }

        // Countdown cooldown bomb: luôn hiển thị mỗi frame
        float remaining = nextBombTime - Time.time;
        if (remaining > 0f)
        {
            hellfireCooldownText.text = ((int)remaining).ToString() + "s";

        }
        else
        {
            hellfireCooldownText.text = "";
        }

        // Nhấn chuột phải để thả bomb nếu đã hồi
        if (Input.GetMouseButtonDown(1))
        {
            if (Time.time >= nextBombTime)
            {
                DropHellfireBombAtMouse();
                nextBombTime = Time.time + bombCooldown;
            }
            else
            {
                Debug.Log("Bom đang hồi, còn " + ((int)remaining).ToString("F1") + "s");
            }
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = firePoint.up * bulletSpeed;  
    }

    void DropHellfireBombAtMouse()
    {
        // Lấy vị trí chuột và chuyển thành toạ độ thế giới
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f; // Quan trọng: set z = 0 nếu đang làm game 2D

        // Tạo bomb tại vị trí chuột
        Instantiate(hellfireBombPrefab, mouseWorldPos, Quaternion.identity);
    }

}
