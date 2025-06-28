using UnityEngine;
using TMPro;
using System;

public class Receiving_Rewards : MonoBehaviour
{
    public GameObject upgradeCanvas;
    public Tank tank;
    //public Animator upgradeAnimator;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
            if (collision.CompareTag("reward_bullet"))
            {
                Debug.Log("Touched: " + collision.name);
                Destroy(collision.gameObject);
            }
        
            if (collision.CompareTag("reward_health"))
            {
                tank.BuffHealth(50);
            Destroy(collision.gameObject);
            }

            if (collision.CompareTag("reward_secretbox"))
            {
                Debug.Log("Touched: " + collision.name);
                Destroy(collision.gameObject);
                //upgradeAnimator.SetBool("isUpgrade", true);
                upgradeCanvas.SetActive(true);
                
            }
    }
}
