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
    public EnemyTankController[] existingEnemies;
    public Transform attackTargetPoint;

    private bool triggered = false;


    public bool IsTriggered 
    {
        get { return triggered; }
    }

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

        StartEnemyAttack(); // Cho enemy có sẵn di chuyển

        foreach (var vision in enemyVisions)
        {
            vision.isEnabled = false; // Tắt chế độ phát hiện sớm
        }

        for (int w = 0; w < waves; w++)
        {
            for (int i = 0; i < enemiesPerWave; i++)
            {
                int index = Random.Range(0, spawnPoints.Length);
                GameObject enemy = Instantiate(enemyPrefab, spawnPoints[index].position, Quaternion.identity);

                // Gọi hàm di chuyển đến mục tiêu nếu có controller
                EnemyTankController controller = enemy.GetComponent<EnemyTankController>();
                if (controller != null && attackTargetPoint != null)
                {
                    controller.attackTargetPoint = attackTargetPoint;
                    controller.StartMovingTo(attackTargetPoint);
                    Debug.Log($"Enemy {i + 1} trong đợt {w + 1} đã được tạo và bắt đầu di chuyển đến mục tiêu.");
                }
            }

            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    void StartEnemyAttack()
    {
        foreach (var enemy in existingEnemies)
        {
            enemy.StartMovingTo(attackTargetPoint);
            Debug.Log($"Enemy {enemy.name} đã bắt đầu di chuyển đến mục tiêu tấn công.");
        }
    }
}
