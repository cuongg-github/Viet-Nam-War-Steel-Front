using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Map1-Tay Nguyen Campaign");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
