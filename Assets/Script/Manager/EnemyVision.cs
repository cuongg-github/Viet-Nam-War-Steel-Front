using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public float detectionTime = 5f;
    private bool isDetecting = false;
    public bool isEnabled = true;
    public TimerDetection timerDetection;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (isEnabled)
        {
            if (other.CompareTag("Tank_Ally"))
            {
                if (!isDetecting)
                {
                    isDetecting = true;
                    timerDetection.StartDetection(detectionTime);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Tank_Ally"))
        {
            isDetecting = false;
            timerDetection.StopDetection();
        }
    }
}
