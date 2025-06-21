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

    private TypewriterEffect typewriterEffect;  // Tham chiếu đến TypewriterEffect

    private AudioManager audioManager;

    void Start()
    {
        // Lấy tham chiếu đến TypewriterEffect từ introText
        typewriterEffect = introText.GetComponent<TypewriterEffect>();
        audioManager = FindObjectOfType<AudioManager>();

        nextButton.SetActive(true);
        skipButton.SetActive(true);    // Đảm bảo nút skip luôn hiện
        introText.text = "";           // Ban đầu không có text nào
        ShowLine();                    // Hiển thị dòng đầu tiên
    }

    // Hàm này sẽ được gọi khi bấm Next
    public void NextIntro()
    {
        if (currentLine < lines.Length)  // Nếu còn dòng để hiển thị
        {
            // Gọi lại DisplayNextText để làm gõ chữ dần cho dòng mới
            typewriterEffect.DisplayNextText(lines[currentLine]);  // Hiển thị dòng hiện tại với hiệu ứng gõ chữ
            currentLine++;  // Tăng chỉ số dòng
        }

        // Khi đã tới dòng cuối cùng
        if (currentLine >= lines.Length)
        {
            // Ẩn text và nút skip
            introText.text = "";
            skipButton.SetActive(false);  // Tắt nút Skip
            nextButton.SetActive(false);  // Tắt nút Next

            // Dừng nhạc cũ và chuyển sang nhạc mới khi chuyển canvas
            if (audioManager != null)
            {
                audioManager.ChangeMusic(audioManager.newMusic);  // Thay đổi nhạc
            }

            // Ẩn canvas hiện tại và hiển thị canvas mới (main menu hoặc gameplay)
            introCanvas.SetActive(false);  // Ẩn canvas giới thiệu
            mainMenuCanvas.SetActive(true); // Hiển thị canvas chính
        }
    }

    public void SkipIntro()
    {
        introText.text = "";
        skipButton.SetActive(false);
        nextButton.SetActive(false);
        introCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);

        // Dừng nhạc khi bấm Skip và chuyển sang nhạc mới
        if (audioManager != null)
        {
            audioManager.StopMusic();  // Tắt nhạc cũ
        }
    }

    // Hàm này hiển thị dòng text và hình ảnh tương ứng
    void ShowLine()
    {
        introImage.sprite = images[currentLine];  // Thay đổi hình ảnh tương ứng
    }
}
