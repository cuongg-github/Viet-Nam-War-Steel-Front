using UnityEngine;

public class EnemyAvoidBlock : MonoBehaviour
{
    public float checkDistance = 1f;
    public float backSpeed = 2f;
    public float backDuration = 0.3f;

    private bool isBacking = false;
    private float backTimer = 0f;

    void Update()
    {
        if (isBacking)
        {
            transform.Translate(-transform.up * backSpeed * Time.deltaTime);
            backTimer -= Time.deltaTime;
            if (backTimer <= 0) isBacking = false;
            return;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, checkDistance, LayerMask.GetMask("BlockMap1"));
        if (hit.collider != null)
        {
            // Vật cản trước mặt, lùi lại
            isBacking = true;
            backTimer = backDuration;
        }
    }
}
