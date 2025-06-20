using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public float detectionTime = 2f;
    private float timer = 0f;
    public GameManager gameManager;
    public bool isEnabled = true;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!isEnabled) return;

        if (other.CompareTag("Tank_Ally"))
        {
            timer += Time.deltaTime;

            if (timer >= detectionTime)
            {
                Debug.Log("Player bị phát hiện!");
                gameManager.OnPlayerDetected();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Tank_Ally"))
        {
            timer = 0f;
        }
    }
}
