using UnityEngine;
using UnityEngine.Events;

public class GunHealth : MonoBehaviour
{
    [SerializeField] int maxHealth;
    int currentHealth;

    public HealthBar healthBar;

    public UnityEvent onDestroyed;

    private void OnEnable()
    {
        onDestroyed.AddListener(DestroySelf);
    }

    private void OnDisable()
    {
        onDestroyed.RemoveListener(DestroySelf);
    }

    private void Start()
    {
        if (maxHealth <= 0)
        {
            Debug.LogError("MaxHealth chưa được gán trong Inspector!");
            return; // hoặc throw error
        }

        currentHealth = maxHealth;
        if (healthBar != null)
            healthBar.UpdateBar(currentHealth, maxHealth);
    }


    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return))
        {
            TakeDamage(20);
        }
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            onDestroyed.Invoke();
        }
        healthBar.UpdateBar(currentHealth, maxHealth);
    }

    public void DestroySelf()
    {
        Destroy(transform.root.gameObject);
    }
}
