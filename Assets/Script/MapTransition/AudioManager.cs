using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public AudioClip newMusic;
    public void ChangeMusic(AudioClip newClip)
    {
        backgroundMusic.Stop();  // Dừng nhạc cũ
        backgroundMusic.clip = newClip;  // Cập nhật nhạc mới
        backgroundMusic.Play();  // Phát nhạc mới
    }

    public void StopMusic()
    {
        backgroundMusic.Stop();
    }
}
