using UnityEngine;

public class TankAIHealth : MonoBehaviour
{
    public bool isShieldActive = false;
    public int maxHealth = 100;
    private int currentHealth;
    public GameObject destroySFX;
    public HealthBar healthSlider;
    public GameObject looseCanvas;
    public Animator looseAnimator;
    public GameObject gameplay;

    private void Start()
    {
        currentHealth = maxHealth;
        healthSlider.UpdateBar(currentHealth, maxHealth);
        looseAnimator.SetBool("isLoose", false);
        looseCanvas.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        if (isShieldActive) return;

        currentHealth -= damage;
        healthSlider.UpdateBar(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Instantiate(destroySFX, transform.position, Quaternion.identity);
        GameOver();
        Destroy(gameObject);
    }

    void GameOver()
    {
        looseAnimator.SetBool("isLoose", true);
        looseCanvas.SetActive(true);
        gameplay.SetActive(false);
    }

}
