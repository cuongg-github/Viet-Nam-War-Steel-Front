using Pathfinding;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float stuckCheckInterval = 1f;
    public float stuckDistance = 0.1f;
    private Vector3 lastPosition;
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= stuckCheckInterval)
        {
            float moved = Vector3.Distance(transform.position, lastPosition);
            if (moved < stuckDistance)
            {
                // Enemy bị kẹt, tìm đường lại
                GetComponent<Seeker>().StartPath(transform.position, GetTargetPosition());
            }

            lastPosition = transform.position;
            timer = 0;
        }
    }

    Vector3 GetTargetPosition()
    {
        // Trả về vị trí mục tiêu (player hoặc điểm nào đó)
        return GameObject.Find("TargetPoint").transform.position;
    }
}

