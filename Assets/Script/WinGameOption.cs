using UnityEngine;
using UnityEngine.SceneManagement;

public class WinGameOption : MonoBehaviour
{
    public void RePlay()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        GameManager.instance.LoadScene(currentSceneIndex);
    }

    public void MainMenu()
    {
        GameManager.instance.LoadScene(1);
    }

    public void NextScene()
    {
        GameManager.instance.NextScene();
    }
}
