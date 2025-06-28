using UnityEngine;

public class Scene : MonoBehaviour
{
    public void NextScene()
    {
        GameManager.instance.NextScene();
    }
}
