using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TimerUI : MonoBehaviour
{
    public TMP_Text timerText;
    public GameManager gameManager;

    void Update()
    {
        if (gameManager == null) return;

        float timeLeft = Mathf.Max(0, gameManager.GetTimeRemaining());
        int minutes = Mathf.FloorToInt(timeLeft / 60);
        int seconds = Mathf.FloorToInt(timeLeft % 60);

        timerText.text = $"Thời gian: {minutes:00}:{seconds:00}";
    }
}
