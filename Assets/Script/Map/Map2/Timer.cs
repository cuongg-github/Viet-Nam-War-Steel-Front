using UnityEngine;
using TMPro;
using Unity.VisualScripting;
public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float timeLimit;
    public GameObject looseCanvas;
    public Animator LooseAnimator;
    public GameObject gameplay_Map2;

    void Start()
    {
        LooseAnimator.SetBool("isLoose",false);
        looseCanvas.SetActive(false);
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
            LooseGame();
        }
    }

    private void LooseGame()
    {
        LooseAnimator.SetBool("isLoose", true);
        looseCanvas.SetActive(true);
        gameplay_Map2.SetActive(false);
    }
}
