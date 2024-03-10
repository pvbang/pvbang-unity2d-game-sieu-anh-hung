using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ContentReincarnation : MonoBehaviour
{
    public GameObject Item_Hero_Frame_My;
    public TextMeshProUGUI textDieuKien;
    public TextMeshProUGUI textHieuQua;

    public GameObject CS1;
    public GameObject CS2;
    public GameObject CS3;
    public GameObject CS4;
    public GameObject CS5;
    public GameObject CS6;
    public GameObject CS7;

    public GameObject itemIcon1;
    public GameObject itemIcon2;
    public GameObject itemIcon3;
    public GameObject itemIcon4;

    public void SetUI(Hero hero)
    {
        if (hero == null) return;

        Item_Hero_Frame_My.SetActive(true);
        Item_Hero_Frame_My.GetComponent<HeroFrame>().SetHeroFrame(hero);

        if (hero.reincarnation == 0) SetCS(false, false, false, false, false, false, false);
        if (hero.reincarnation == 1) SetCS(true, false, false, false, false, false, false);
        if (hero.reincarnation == 2) SetCS(true, true, false, false, false, false, false);
        if (hero.reincarnation == 3) SetCS(true, true, true, false, false, false, false);
        if (hero.reincarnation == 4) SetCS(true, true, true, true, false, false, false);
        if (hero.reincarnation == 5) SetCS(true, true, true, true, true, false, false);
        if (hero.reincarnation == 6) SetCS(true, true, true, true, true, true, false);
        if (hero.reincarnation == 7) SetCS(true, true, true, true, true, true, true);
    }

    void SetCS(bool cs1, bool cs2, bool cs3, bool cs4, bool cs5, bool cs6, bool cs7)
    {
        CS1.SetActive(cs1);
        CS2.SetActive(cs2);
        CS3.SetActive(cs3);
        CS4.SetActive(cs4);
        CS5.SetActive(cs5);
        CS6.SetActive(cs6);
        CS7.SetActive(cs7);
    }
}
