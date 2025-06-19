using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseButton;
    public GameObject pauseScreen;

    private bool isPaused = false;

    public void PauseGame()
    {
        //Debug.Log("Game Paused");
        isPaused = true;
        Time.timeScale = 0f;
        pauseButton.SetActive(false);
        pauseScreen.SetActive(true);
    }

    public void ResumeGame()
    {
        //Debug.Log("Game Continue");
        isPaused = false;
        Time.timeScale = 1f;
        pauseButton.SetActive(true);
        pauseScreen.SetActive(false);
    }
}
