using UnityEngine;

public class MainMenuSelectMap : MonoBehaviour
{
    public void Map1()
    {
        GameManager.instance.LoadScene(2);
    }

    public void Map2()
    {
        GameManager.instance.LoadScene(4);
    }

    public void Map3()
    {
        GameManager.instance.LoadScene(6);
    }

}
