using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    public Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void UpdateHP(float health, float maxHealth)
    {
        image.fillAmount = health / maxHealth;
    }
}
