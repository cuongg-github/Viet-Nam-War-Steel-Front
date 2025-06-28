using System.Collections;
using TMPro;
using UnityEngine;

public class TimerDefend : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public GameObject defendUI;
    public GameObject canvas;
    private float defendTime = 0f;
    private float currentTime = 0f;
    private bool isCounting = false;
    private bool isPreparing = false;
    private float prepareTime = 0f;
    public GameManagerMap1 gameManager;

    void Start()
    {
        canvas.SetActive(true);
        defendUI.SetActive(false);
        isCounting = false;
        isPreparing = false;
    }

    public void StartPrepare(float time)
    {
        prepareTime = time;
        currentTime = time;
        isPreparing = true;
        isCounting = false;
        canvas.SetActive(true);
        defendUI.SetActive(true);
    }

    // Pha phòng thủ chính
    public void StartDefend(float time)
    {
        defendTime = time;
        currentTime = time;
        isPreparing = false;
        isCounting = true;
        canvas.SetActive(true);
        defendUI.SetActive(true);
    }

    void StopDefend()
    {
        isCounting = false;
        defendUI.SetActive(false);
        canvas.SetActive(false);
        Debug.Log("[TimerDefend] Kết thúc phòng thủ");
    }

    void Update()
    {
        if (!isCounting && !isPreparing) return;

        currentTime -= Time.deltaTime;
        currentTime = Mathf.Clamp(currentTime, 0f, isPreparing ? prepareTime : defendTime);


        int seconds = Mathf.CeilToInt(currentTime);

        if (timerText == null)
        {
            return;
        }

        if (isPreparing)
        {
            timerText.text = $"Chuẩn bị địch tấn công trong: {seconds}s";
            if (currentTime <= 0f)
            {
                isPreparing = false;
                isCounting = false;
            }
        }
        else if (isCounting)
        {
            timerText.text = $"Phòng thủ còn: {seconds}s";
            if (currentTime <= 0f)
            {
                StopDefend();
                gameManager.OnDefendSuccessfull();
            }
        }
    }

    public IEnumerator StartPrepareCoroutine(float time)
    {
        currentTime = time;
        isPreparing = true;
        isCounting = false;
        canvas.SetActive(true);
        defendUI.SetActive(true);

        while (currentTime > 0f)
        {
            currentTime -= Time.deltaTime;
            currentTime = Mathf.Clamp(currentTime, 0f, time);
            int seconds = Mathf.CeilToInt(currentTime);
            if (timerText != null)
                timerText.text = $"Chuẩn bị địch tấn công trong: {seconds}s";

            yield return null;
        }

        isPreparing = false;
        defendUI.SetActive(false); 
    }


}
