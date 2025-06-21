using UnityEngine;

public class OnDestroy : MonoBehaviour
{
    void Start()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
        Destroy(gameObject, audio.clip.length);
    }
}
