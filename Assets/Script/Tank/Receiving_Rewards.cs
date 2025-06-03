using UnityEngine;
using TMPro;

public class Receiving_Rewards : MonoBehaviour
{
    public int health = 100;
    public int bullet = 10;
    public TextMeshProUGUI bulletText;
    public TextMeshProUGUI healthText;
    void Start()
    {
        bulletText.SetText(bullet.ToString());
        healthText.SetText(health.ToString());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("reward_bullet"))
        {
            bullet += 3;
            bulletText.SetText(bullet.ToString());
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("reward_health"))
        {
            health += 10;
            healthText.SetText(health.ToString());
            Destroy(collision.gameObject);

        }
    }
}
