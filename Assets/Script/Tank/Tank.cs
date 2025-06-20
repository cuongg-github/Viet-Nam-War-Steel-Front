using UnityEngine;

public class Tank : MonoBehaviour
{

    public Animator trackLeft;
    public Animator trackRight;
    public Rigidbody2D rb;
    public PolygonCollider2D poly;
    public int maxHealth = 100;
    public float moveSpeed = 3f;
    public float rotateSpeed = 100f;
    public AudioClip movementSound;
    public GameObject destroySFX;
    public HealthSlider healthSlider;
    private AudioSource audioSource;
    private int currentHealth;
    Vector2 moveAmount;
    public float doublerotate = 10f;
    public GameObject looseCanvas;
    public Animator LooseAnimator;
    public GameObject gameplay_Map2;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        LooseAnimator.SetBool("isLoose", false);
        looseCanvas.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
        healthSlider.SetMaxHealth(maxHealth);
        rb.angularDamping = 10f;
        rb.linearDamping = 10f;
        rb.gravityScale = 0;
        poly = GetComponent<PolygonCollider2D>();
        float directionX = Mathf.Sign(transform.localScale.x);
    }



    void Update()
    {
        moveAmount = transform.up * Input.GetAxisRaw("Vertical");
    }
    void FixedUpdate()
    {
        if ( Input.GetAxisRaw("Vertical") != 0 )
        {
            rb.MovePosition(rb.position + moveAmount * moveSpeed * Time.fixedDeltaTime);
            if ( Input.GetAxisRaw("Horizontal") != 0 )
            {
                if (Input.GetAxisRaw("Vertical") == 1 )
                {
                    rb.MoveRotation(rb.rotation - Input.GetAxisRaw("Horizontal") * rotateSpeed * Time.fixedDeltaTime);
                } else
                {
                    rb.MoveRotation(rb.rotation + Input.GetAxisRaw("Horizontal") * rotateSpeed * Time.fixedDeltaTime);
                }
            }
            trackStart();
        } else
        {
            trackStop();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthSlider.SetHeath(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Instantiate(destroySFX, transform.position, Quaternion.identity);
        LooseGame();
        Destroy(gameObject);
    }

    private void LooseGame()
    {
        LooseAnimator.SetBool("isLoose", true);
        looseCanvas.SetActive(true);
        gameplay_Map2.SetActive(false);
    }

    void trackStart()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = movementSound;
            audioSource.loop = true;
            audioSource.Play();
        }

        trackLeft.SetBool("isMoving", true);
        trackRight.SetBool("isMoving", true);
    }

    void trackStop()
    {
        if (audioSource.clip == movementSound)
        {
            audioSource.Stop();
            audioSource.clip = null;
        }

        trackLeft.SetBool("isMoving", false);
        trackRight.SetBool("isMoving", false);
    }

}