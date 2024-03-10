using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemHeroBanner : MonoBehaviour
{
    public TextMeshProUGUI textLevel;
    public TextMeshProUGUI textNameHero;
    public TextMeshProUGUI textNotification;

    public Image icon;
    public Image iconFrame;

    public Sprite superEvolution2;
    public Sprite superEvolution6; 
    public Sprite superEvolution8;
    public Sprite superEvolution12;
    public Sprite superEvolution14;
    public Sprite superEvolution17;

    public void SetUI(int level, string nameHero, string notification, Sprite icon, Sprite iconFrame, int superEvolution, int reincarnation)
    {
        textLevel.text = level.ToString();

        string textReincarnation = nameHero;
        if (reincarnation == 1) textReincarnation = "Dũng sĩ" + " - " + nameHero;
        else if (reincarnation == 2) textReincarnation = "Hiệp sĩ" + " - " + nameHero;
        else if (reincarnation == 3) textReincarnation = "Anh hùng" + " - " + nameHero;
        else if (reincarnation == 4) textReincarnation = "Thần" + " - " + nameHero;
        else if (reincarnation == 5) textReincarnation = "Thánh" + " - " + nameHero;
        else if (reincarnation == 6) textReincarnation = "Linh" + " - " + nameHero;
        else if (reincarnation == 7) textReincarnation = "Hồn" + " - " + nameHero;


        textNameHero.text = textReincarnation;
        textNotification.text = notification;
        this.icon.sprite = icon;

        if (superEvolution < 2)
        {
            this.iconFrame.sprite = iconFrame;
        }
        else if (superEvolution >= 2 && superEvolution < 6)
        {
            this.iconFrame.sprite = superEvolution2;
        } 
        else if (superEvolution >= 6 && superEvolution < 8)
        {
            this.iconFrame.sprite = superEvolution6;
        }
        else if (superEvolution >= 8 && superEvolution < 12)
        {
            this.iconFrame.sprite = superEvolution8;
        }
        else if (superEvolution >= 12 && superEvolution < 14)
        {
            this.iconFrame.sprite = superEvolution12;
        }
        else if (superEvolution >= 14 && superEvolution < 17)
        {
            this.iconFrame.sprite = superEvolution14;
        }
        else if (superEvolution >= 17)
        {
            this.iconFrame.sprite = superEvolution17;
        }
    }
}
