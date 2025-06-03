using UnityEngine;

public class Receiving_Rewards : MonoBehaviour
{
    public int health = 100;
    public int bullet = 10;
    void Start()
    {
        
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
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("reward_health"))
        {
            health += 10;
            Destroy(collision.gameObject);

        }
    }
}
