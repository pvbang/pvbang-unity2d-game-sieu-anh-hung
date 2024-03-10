using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TeamInfo : MonoBehaviour
{
    // hp
    public TextMeshProUGUI hpDefault;
    public TextMeshProUGUI hpPlus;
    
    // tank
    public TextMeshProUGUI tankDefault;
    public TextMeshProUGUI tankPlus;

    // speed
    public TextMeshProUGUI speedDefault;
    public TextMeshProUGUI speedPlus;

    // damagePhysical
    public TextMeshProUGUI damagePhysicalDefault;
    public TextMeshProUGUI damagePhysicalPlus;

    // damageMagic
    public TextMeshProUGUI damageMagicDefault;
    public TextMeshProUGUI damageMagicPlus;

    public void SetUIDefault(ulong hp, ulong tank, ulong speed, ulong damagePhysical, ulong damageMagic)
    {
        hpDefault.text = hp.ToString();
        tankDefault.text = tank.ToString();
        speedDefault.text = speed.ToString();
        damagePhysicalDefault.text = damagePhysical.ToString();
        damageMagicDefault.text = damageMagic.ToString();
    }

    public void SetUIDefaultBlank()
    {
        hpDefault.text = "0";
        tankDefault.text = "0";
        speedDefault.text = "0";
        damagePhysicalDefault.text = "0";
        damageMagicDefault.text = "0";
    }

    public void SetUIPlus(ulong hp, ulong tank, ulong speed, ulong damagePhysical, ulong damageMagic)
    {
        hpPlus.text = hp.ToString();
        tankPlus.text = tank.ToString();
        speedPlus.text = speed.ToString();
        damagePhysicalPlus.text = damagePhysical.ToString();
        damageMagicPlus.text = damageMagic.ToString();
    }
}
