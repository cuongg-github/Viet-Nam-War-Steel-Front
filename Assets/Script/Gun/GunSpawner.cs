using UnityEngine;

public enum GameDifficulty
{
    Easy,
    Medium,
    Hard   
}
public class GunSpawner : MonoBehaviour
{
    [SerializeField] private GameObject gunPrefab;        
    [SerializeField] private Transform[] spawnPoints;   

    void Start()
    {
        SpawnGun();
    }

    void SpawnGun()
    {
        foreach (Transform point in spawnPoints)
        {
            Instantiate(gunPrefab, point.position, point.rotation);
        }
    }
}
