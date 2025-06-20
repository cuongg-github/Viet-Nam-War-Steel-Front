using UnityEngine;

public class EnemyAI_MoveToTarget : MonoBehaviour
{
    public Transform target;
    public float speed = 2f;

    private bool canMove = false;

    void Start()
    {

        if (target == null)
        {
            GameObject found = GameObject.Find("TargetPoint");
            if (found != null)
            {
                target = found.transform;
                canMove = true;
            }
            else
            {
                Debug.LogWarning("Không tìm thấy TargetPoint trong scene!");
            }
        }
    }

    public void StartMovingTo(Transform destination)
    {
        target = destination;
        canMove = true;
    }

    void Update()
    {
        if (canMove && target != null)
        {
            Vector3 dir = (target.position - transform.position).normalized;
            transform.position += dir * speed * Time.deltaTime;
        }
    }
}
