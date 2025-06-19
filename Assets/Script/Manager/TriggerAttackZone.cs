using UnityEngine;
using System.Collections;

public class TriggerAttackZone : MonoBehaviour
{
    public float delayBeforeStart = 5f;
    public Transform[] spawnPoints;
    public GameObject enemyPrefab;
    public int waves = 3;
    public float timeBetweenWaves = 10f;
    public int enemiesPerWave = 3;
    public EnemyVision[] enemyVisions;
    public EnemyAI_MoveToTarget[] existingEnemies;
    public Transform attackTargetPoint;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag("Tank_Ally"))
        {
            triggered = true;
            Debug.Log("Ally vào vùng – Đang chờ địch phản ứng...");
            StartCoroutine(StartEnemyWaves());
        }
    }

    IEnumerator StartEnemyWaves()
    {
        yield return new WaitForSeconds(delayBeforeStart);

        Debug.Log("Địch bắt đầu tấn công theo đợt!");

        StartEnemyAttack();
        foreach (var vision in enemyVisions)
        {
            vision.isEnabled = false;
        }

        for (int w = 0; w < waves; w++)
        {
            for (int i = 0; i < enemiesPerWave; i++)
            {
                int index = Random.Range(0, spawnPoints.Length);
                Instantiate(enemyPrefab, spawnPoints[index].position, Quaternion.identity);
            }

            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    void StartEnemyAttack()
    {
        foreach (var enemy in existingEnemies)
        {
            enemy.StartMovingTo(attackTargetPoint);
        }
    }
}
