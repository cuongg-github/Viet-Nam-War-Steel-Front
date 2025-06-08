using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image RedBar;
    public TextMeshProUGUI HealthText;

    public void UpdateBar(int currentValue, int maxValue)
    {
        RedBar.fillAmount = maxValue > 0 ? (float)currentValue / maxValue : 0f;
        HealthText.text = currentValue.ToString() + " / " + maxValue.ToString();
    }
}
