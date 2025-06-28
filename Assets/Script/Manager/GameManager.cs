using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerr : MonoBehaviour
{

    public GameObject WinCanvas;
    public Animator WinAnimator;
    public GameObject LoseCanvas;
    public Animator LoseAnimator;

    private bool gameEnded = false;

    void WinGame()
    {
        gameEnded = true;
        WinAnimator.SetBool("isLoose", false);
        WinCanvas.SetActive(true);
    }

    void LoseGame()
    {
        gameEnded = true;
        LoseAnimator.SetBool("isLoose", true);
        LoseCanvas.SetActive(true);
    }

    public void OnPlayerDetected()
    {
         Debug.Log("Bị phát hiện gameover");
         LoseGame();
    }

    public void OnDefendSuccessfull()
    {
        Debug.Log("Phòng thủ thành công");
        WinGame();
    }
}
