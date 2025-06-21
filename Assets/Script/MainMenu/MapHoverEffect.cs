using UnityEngine;
using UnityEngine.UI;  // Cần để sử dụng Image component của Panel hoặc Button
using UnityEngine.EventSystems;

public class MapHoverEffect : MonoBehaviour
{
    // Lưu các Image component của các Panel và Button con trong Map 1
    private Image[] childImages;
    private Vector3 normalScale;
    private Color normalColor;
    public Color hoverColor = Color.green;  // Màu khi hover
    public Vector3 hoverScale = new Vector3(1.1f, 1.1f, 1f);  // Kích thước khi hover

    private void Start()
    {
        // Lấy tất cả Image component của các đối tượng con trong Map 1
        childImages = GetComponentsInChildren<Image>();  // Lấy Image component từ tất cả các con của Map 1

        // Lưu lại màu sắc và kích thước ban đầu của Map 1
        if (childImages.Length > 0)
        {
            normalColor = childImages[0].color;  // Giả sử tất cả các Image có màu sắc ban đầu giống nhau
        }

        normalScale = transform.localScale;  // Lưu lại kích thước ban đầu của Map 1
    }

    // Khi chuột di vào (hover) vào đối tượng
    private void OnMouseEnter()
    {
        // Thay đổi màu cho tất cả Image component của các đối tượng con (Panel và Button)
        foreach (var img in childImages)
        {
            img.color = hoverColor;  // Thay đổi màu khi hover vào
        }

        // Phóng to khi hover
        transform.localScale = hoverScale;  // Phóng to Map 1
    }

    // Khi chuột rời khỏi đối tượng
    private void OnMouseExit()
    {
        // Quay lại màu sắc ban đầu cho tất cả Image component của các đối tượng con
        foreach (var img in childImages)
        {
            img.color = normalColor;  // Quay lại màu ban đầu khi không hover
        }

        // Quay lại kích thước ban đầu của Map 1
        transform.localScale = normalScale;  // Quay lại kích thước ban đầu
    }
}
