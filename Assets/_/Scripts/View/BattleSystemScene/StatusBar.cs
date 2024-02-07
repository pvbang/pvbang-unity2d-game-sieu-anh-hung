using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    public Image _HP;

    public void updateHP(float currentHP, float totalHP)
    {
        _HP.fillAmount = currentHP / totalHP;
    }
}
