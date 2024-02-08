using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Image fillBar;
    public TextMeshProUGUI healthText;

    private void Awake()
    {
        fillBar = transform.Find("Fill").GetComponent<Image>();
        healthText = transform.Find("Text").GetComponent<TextMeshProUGUI>();
    }

    public void UpdateBar(float health, float maxHealth)
    {
        fillBar.fillAmount = health / maxHealth;
        healthText.text = health.ToString() + " / " + maxHealth.ToString();
    }
}
