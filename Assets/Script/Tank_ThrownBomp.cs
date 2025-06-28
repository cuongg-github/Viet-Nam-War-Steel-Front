using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tank_ThrownBomp : MonoBehaviour
{
    public GameObject hellfireBombPrefab;
    public TextMeshProUGUI hellfireCooldownText;

    public Image BombIcon;
    public Image cooldownImage;
    public float bombCooldown = 15f;
    private float nextBombTime = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cooldownImage.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float remaining = nextBombTime - Time.time;
        if (remaining > 0f)
        {
            float fill = Mathf.Clamp01(remaining / bombCooldown);
            cooldownImage.fillAmount = fill;
            hellfireCooldownText.text = Mathf.CeilToInt(remaining).ToString();
        } else
        {
            hellfireCooldownText.text = "";
        }
        if (Input.GetMouseButtonDown(1) && Time.time >= nextBombTime)
        {
            DropHellfireBombAtMouse();
            nextBombTime = Time.time + bombCooldown;
            cooldownImage.fillAmount = 1f;
        }
    }

    void DropHellfireBombAtMouse()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;
        Instantiate(hellfireBombPrefab, mouseWorldPos, Quaternion.identity);
    }
}
