using UnityEngine;

public class MainCanvasAudioManager : MonoBehaviour
{
    private AudioManager audioManager;
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void PlayGame()
    {
        audioManager.StopMusic();
    }
}
