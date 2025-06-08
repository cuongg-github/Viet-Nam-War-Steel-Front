using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int requiredGunsDestroyed = 3;
    public int requiredTanksDestroyed = 5;
    public float timeLimit = 120f; 

    public GameObject winPanel;
    public GameObject losePanel;

    private int currentGunsDestroyed = 0;
    private int currentTanksDestroyed = 0;
    private float timer;

    private bool gameEnded = false;

    void Start()
    {
        timer = timeLimit;
    }

    void Update()
    {
        if (gameEnded) return;

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            LoseGame();
        }

        if (currentGunsDestroyed >= requiredGunsDestroyed &&
            currentTanksDestroyed >= requiredTanksDestroyed)
        {
            WinGame();
        }
    }

    public void AddGunDestroyed()
    {
        currentGunsDestroyed++;
    }

    public void AddTankDestroyed()
    {
        currentTanksDestroyed++;
    }

    void WinGame()
    {
        gameEnded = true;
        Debug.Log("WIN GAME!");
        if (winPanel != null) winPanel.SetActive(true);
        //Invoke("LoadNextLevel", 5f);
    }

    void LoseGame()
    {
        gameEnded = true;
        Debug.Log("YOU LOSE!");
        if (losePanel != null) losePanel.SetActive(true);
        Invoke("ReloadLevel", 5f);
    }

    //void LoadNextLevel()
    //{
    //    SceneManager.LoadScene("Map2");
    //}

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public float GetTimeRemaining()
    {
        return timer;
    }
}
