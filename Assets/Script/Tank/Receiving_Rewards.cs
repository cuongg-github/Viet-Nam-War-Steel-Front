using UnityEngine;
using TMPro;
using System;

public class Receiving_Rewards : MonoBehaviour
{
    public int health = 100;
    public int bullet = 10;
    public TextMeshProUGUI bulletText;
    public TextMeshProUGUI healthText;
    int hasBullet, hasHealth;
    
    void Start()
    {
        bulletText.SetText(bullet.ToString());
        healthText.SetText(health.ToString());
        System.Random random = new System.Random();
        hasBullet = random.Next(2);
        hasHealth = random.Next(2);
        Debug.Log(hasBullet+ " " + hasHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasBullet == 1)
        {
            if (collision.CompareTag("reward_bullet"))
            {
                bullet += 3;
                bulletText.SetText(bullet.ToString());
                Destroy(collision.gameObject);
            }
        }
    if (hasHealth == 1)
        {
            if (collision.CompareTag("reward_health"))
            {
                health += 10;
                healthText.SetText(health.ToString());
                Destroy(collision.gameObject);
            }
        }
    }
}
