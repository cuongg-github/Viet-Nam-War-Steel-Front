using UnityEngine;

public class onDestroyObject : MonoBehaviour
{
    void Start()
    {
        float animDuration = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        Destroy(gameObject, animDuration);
    }
}
