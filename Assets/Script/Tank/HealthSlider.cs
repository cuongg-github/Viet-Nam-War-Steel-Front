using UnityEngine;
using UnityEngine.UI;
public class HealthSlider : MonoBehaviour
{
    public Slider healthSlider;

    public void SetMaxHealth( int maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth; 
    }

    public void SetHeath(int health)
    {
        healthSlider.value = health;
    }
}
