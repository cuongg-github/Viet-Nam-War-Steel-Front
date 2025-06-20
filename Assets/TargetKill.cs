using UnityEngine;
using TMPro;
public class TargetKill : MonoBehaviour
{
    public TextMeshProUGUI tankText;
    public TextMeshProUGUI turretText;
    public int targetTank;
    public int targetTurret;

    private int currentTank;
    private int currentTurret;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void KilledTankEnemy()
    {
        currentTank++;
        tankText.text = currentTank.ToString() + "/" + targetTank.ToString();
    }
}
