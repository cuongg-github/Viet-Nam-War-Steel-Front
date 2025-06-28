using TMPro;
using UnityEngine;

public class TimerDetection : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public GameObject detectionUI;
    public GameObject canvas;
    private float detectionTime = 0f;
    private float maxDetectionTime = 0f;
    private bool isCounting = false;

    public GameManager gameManager;

    void Start()
    {
        canvas.SetActive(true);
        detectionUI.SetActive(false);
    }

    public void StartDetection(float duration)
    {
        if (timerText == null)
        {
            Debug.LogError("❌ timerText chưa được gán trong TimerDetection!");
        }
        maxDetectionTime = duration;
        detectionTime = duration;
        isCounting = true;
        detectionUI.SetActive(true);
        canvas.SetActive(true);
    }

    public void StopDetection()
    {
        isCounting = false;
        detectionUI.SetActive(false);
        canvas.SetActive(false);
    }

    void Update()
    {
        if (!isCounting) return;

        detectionTime -= Time.deltaTime;
        detectionTime = Mathf.Clamp(detectionTime, 0f, maxDetectionTime);

        int seconds = Mathf.CeilToInt(detectionTime);
        timerText.text = $"Bị phát hiện sau: {seconds}s";

        if (detectionTime <= 0f)
        {
            StopDetection();
            gameManager.OnPlayerDetected(); 
        }
    }
}
