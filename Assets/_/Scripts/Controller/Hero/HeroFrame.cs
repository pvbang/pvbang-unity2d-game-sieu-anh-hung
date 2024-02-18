using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeroFrame : MonoBehaviour
{
    public Image background;
    public Image image;
    public TextMeshProUGUI heroLevel;
    public TextMeshProUGUI heroName;

    private void Awake()
    {
        if (background == null)
        {
            background = transform.Find("Background").GetComponent<Image>();
        }
        if (image == null)
        {
            image = transform.Find("Image").GetComponent<Image>();
        }
        if (heroLevel == null)
        {
            heroLevel = transform.Find("Level").GetComponent<TextMeshProUGUI>();
        }
        if (heroName == null)
        {
            heroName = transform.Find("Name").GetComponent<TextMeshProUGUI>();
        }
    }

    public void SetHeroFrame(Sprite background, Sprite image, string heroLevel, string heroName)
    {
        this.background.sprite = background;
        this.image.sprite = image;
        this.heroLevel.text = heroLevel;
        this.heroName.text = heroName;
    }

    public void SetHeroFrame(Hero hero)
    {
        GameObject heroObject = GameAssets.Instance.GetGameObjectFromId(hero.h_id);
        if (heroObject != null)
        {
            ItemAssets itemAssets = heroObject.GetComponent<ItemAssets>();
            if (itemAssets != null)
            {
                this.background.sprite = itemAssets.GetBackground();
                this.image.sprite = itemAssets.GetImage();
            }
        }
        this.heroLevel.text = "Lv. " + hero.h_level.ToString() + " / " + hero.h_levelMax.ToString();
        this.heroName.text = hero.h_name;
    }
}
