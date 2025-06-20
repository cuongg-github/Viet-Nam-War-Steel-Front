using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public TextMeshProUGUI introText;  // TextMeshPro để hiển thị dòng text
    public GameObject skipButton;      // Nút Skip
    public GameObject nextButton;      // Nút Next
    public Image introImage;           // Image hiển thị hình ảnh đi kèm với text
    public GameObject introCanvas;
    public GameObject mainMenuCanvas;
    [TextArea]
    public string[] lines;             // Mảng các dòng text cần hiển thị
    public Sprite[] images;            // Mảng hình ảnh đi kèm mỗi dòng
    private int currentLine = 0;       // Dòng hiện tại

    private AudioManager audioManager;
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        nextButton.SetActive(true);
        skipButton.SetActive(true);    // Đảm bảo nút skip luôn hiện
        introText.text = "";           // Ban đầu không có text nào
        ShowLine();                    // Hiển thị dòng đầu tiên
    }

    // Hàm này sẽ được gọi khi bấm Skip
    public void NextIntro()
    {
        if (currentLine < lines.Length)  // Nếu còn dòng để hiển thị
        {
            ShowLine();  // Hiển thị dòng hiện tại
            currentLine++;  // Tăng chỉ số dòng
        }

        // Khi đã tới dòng cuối cùng
        if (currentLine >= lines.Length)
        {
            // Ẩn text và nút skip
            introText.text = "";
            skipButton.SetActive(false);  // Tắt nút Skip
            nextButton.SetActive(false);
            introCanvas.SetActive(false);  // Ẩn canvas giới thiệu
            mainMenuCanvas.SetActive(true); // Hiển thị canvas chính
            audioManager.StopMusic();
        }
    }
    public void SkipIntro()
    {
        introText.text = "";
        skipButton.SetActive(false);
        nextButton.SetActive(false);
        introCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
        audioManager.StopMusic();


    }

    void ShowLine()
    {
        introText.text = lines[currentLine]; 
        introImage.sprite = images[currentLine];  // Thay đổi hình ảnh tương ứng
    }
}
