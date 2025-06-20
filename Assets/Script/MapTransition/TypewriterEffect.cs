using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TypewriterEffect : MonoBehaviour
{
    public float typingSpeed = 0.05f;  // Tốc độ gõ chữ
    private string fullText;
    private string currentText = "";
    public TextMeshProUGUI textComponent;

    void Start()
    {
        fullText = textComponent.text;  // Lấy text gốc
        textComponent.text = "";        // Xóa text ban đầu
    }

    // Hàm này sẽ được gọi mỗi khi bấm Next
    public void DisplayNextText(string newText)
    {
        fullText = newText;           // Cập nhật lại text mới
        currentText = "";             // Reset text hiện tại
        StartCoroutine(TypeText());   // Bắt đầu hiệu ứng gõ chữ mới
    }

    IEnumerator TypeText()
    {
        foreach (char letter in fullText.ToCharArray())
        {
            currentText += letter;       // Thêm ký tự vào text hiện tại
            textComponent.text = currentText;  // Cập nhật text
            yield return new WaitForSeconds(typingSpeed);  // Chờ trước khi thêm ký tự tiếp theo
        }
    }
}
