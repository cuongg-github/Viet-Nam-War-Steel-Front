using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class TypewriterEffect : MonoBehaviour
{
    public float typingSpeed = 0.05f;  // Tốc độ gõ chữ
    private string fullText;
    private string currentText = "";
    public TextMeshProUGUI textComponent;  // TextMeshPro để hiển thị dòng text
    public Image introImage;  // Image hiển thị hình ảnh đi kèm với text

    private Coroutine typingCoroutine;  // Tham chiếu đến Coroutine gõ chữ
    private bool isTyping = false;  // Biến để kiểm tra nếu có gõ chữ

    void Start()
    {
        fullText = textComponent.text;  // Lấy text gốc
        textComponent.text = "";        // Xóa text ban đầu
    }

    // Hàm này sẽ được gọi mỗi khi bấm Next
    public void DisplayNextText(string newText, Sprite newImage)
    {
        if (isTyping)
        {
            // Nếu đang gõ chữ, dừng hiệu ứng và hiển thị toàn bộ text
            StopCoroutine(typingCoroutine);
            textComponent.text = newText;  // Hiển thị toàn bộ text
            introImage.sprite = newImage;  // Cập nhật hình ảnh
            isTyping = false;  // Đánh dấu là không còn gõ chữ nữa
        }
        else
        {
            fullText = newText;  // Cập nhật lại text mới
            currentText = "";     // Reset text hiện tại
            typingCoroutine = StartCoroutine(TypeText(newImage));  // Bắt đầu hiệu ứng gõ chữ mới với hình ảnh mới
        }
    }

    // Coroutine để gõ chữ từ từ
    IEnumerator TypeText(Sprite newImage)
    {
        introImage.sprite = newImage;  // Thay đổi hình ảnh tương ứng với dòng text mới
        isTyping = true;  // Đánh dấu là đang gõ chữ

        foreach (char letter in fullText.ToCharArray())
        {
            currentText += letter;       // Thêm ký tự vào text hiện tại
            textComponent.text = currentText;  // Cập nhật text
            yield return new WaitForSeconds(typingSpeed);  // Chờ trước khi thêm ký tự tiếp theo
        }

        isTyping = false;  // Đánh dấu kết thúc việc gõ chữ
    }
}
