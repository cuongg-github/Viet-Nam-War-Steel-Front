using UnityEngine;
using TMPro;
using System.Collections;
public class TargetKill : MonoBehaviour
{
    public TextMeshProUGUI tankText;
    public GameObject gameplay_Map2;
    public int targetTank;
    private int currentTank;
    public GameObject winCanvas;
    public Animator WinAnimator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        WinAnimator.SetBool("isWin", false);
        winCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTank >= targetTank)
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        WinAnimator.SetBool("isWin",true);
        winCanvas.SetActive(true);
        gameplay_Map2.SetActive(false);
    }
    public void KilledTankEnemy()
    {
        currentTank++;
        tankText.text = currentTank.ToString() + "/" + targetTank.ToString();
    }
}
