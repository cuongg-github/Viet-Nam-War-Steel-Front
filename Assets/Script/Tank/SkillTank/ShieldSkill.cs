using UnityEngine;

public class ShieldSkill : MonoBehaviour
{
    public GameObject shieldVisual; 
    public float shieldDuration = 15f; 
    private bool isShieldActive = false;
    private float shieldTimer = 0f;

    void Start()
    {
        if (shieldVisual != null)
            shieldVisual.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isShieldActive)
        {
            ActivateShield();
        }

        if (isShieldActive)
        {
            shieldTimer -= Time.deltaTime;

            if (shieldTimer <= 0f)
            {
                DeactivateShield();
            }
        }
    }

    void ActivateShield()
    {
        if (shieldVisual != null)
            shieldVisual.SetActive(true);

        isShieldActive = true;
        shieldTimer = shieldDuration;

        // Báo cho tankAI
        GetComponent<TankAIHealth>().isShieldActive = true;
    }

    void DeactivateShield()
    {
        if (shieldVisual != null)
            shieldVisual.SetActive(false);

        isShieldActive = false;

        // Tắt bảo vệ
        GetComponent<TankAIHealth>().isShieldActive = false;
    }
}
