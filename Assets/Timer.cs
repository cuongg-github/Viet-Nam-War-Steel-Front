using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float timeLimit;
    public GameObject looseCanvas;

    void Start()
    {

    }
    void Update()
    {
        if (timeLimit > 0)
        {
            timeLimit -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(timeLimit / 60);
            int seconds = Mathf.FloorToInt(timeLimit % 60);
            timerText.text = $"{minutes:00}:{seconds:00}";
        } else if ( timeLimit <= 0 )
        {

        }
    }
}
