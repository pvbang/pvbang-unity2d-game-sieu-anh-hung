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

    public Sprite frameWhite;
    public Sprite frameGray;
    public Sprite frameGreen;
    public Sprite frameBlue;
    public Sprite frameViolet;
    public Sprite frameOrange1;
    public Sprite frameOrange2;
    public Sprite frameOrange3;
    public Sprite frameRed1;
    public Sprite frameRed2;
    public Sprite frameRed3;
    public Sprite frameYellow1;
    public Sprite frameYellow2;
    public Sprite frameYellow3;

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
                this.background.sprite = GetFrameBackground(itemAssets, hero);
                this.image.sprite = itemAssets.GetImage();
            }
        }
        this.heroLevel.text = "Lv. " + hero.h_level.ToString() + " / " + hero.h_levelMax.ToString();
        this.heroName.text = hero.h_name;
    }

    public Sprite GetFrameBackground(ItemAssets itemAssets, Hero hero)
    {
        string color = itemAssets.GetColor();


        if (color == "White")
        {
            if (hero.superEvolution <= 2) return frameWhite;
            else if (hero.superEvolution <= 4) return frameGreen;
            else if (hero.superEvolution <= 6) return frameBlue;
            else if (hero.superEvolution <= 8) return frameViolet;
            else if (hero.superEvolution <= 10) return frameOrange1;
            else if (hero.superEvolution <= 12) return frameOrange2;
            else if (hero.superEvolution <= 14) return frameOrange3;
            else if (hero.superEvolution <= 16) return frameRed1;
            else return frameRed2;
        }
        else if (color == "Gray")
        {
            if (hero.superEvolution <= 2) return frameGray;
            else if (hero.superEvolution <= 4) return frameGreen;
            else if (hero.superEvolution <= 6) return frameBlue;
            else if (hero.superEvolution <= 8) return frameViolet;
            else if (hero.superEvolution <= 10) return frameOrange1;
            else if (hero.superEvolution <= 12) return frameOrange2;
            else if (hero.superEvolution <= 14) return frameOrange3;
            else if (hero.superEvolution <= 16) return frameRed1;
            else return frameRed2;
        }
        else if (color == "Green")
        {
            if (hero.superEvolution <= 2) return frameGreen;
            else if (hero.superEvolution <= 4) return frameBlue;
            else if (hero.superEvolution <= 6) return frameViolet;
            else if (hero.superEvolution <= 8) return frameOrange1;
            else if (hero.superEvolution <= 10) return frameOrange2;
            else if (hero.superEvolution <= 12) return frameOrange3;
            else if (hero.superEvolution <= 14) return frameRed1;
            else if (hero.superEvolution <= 16) return frameRed2;
            else return frameRed3;
        }
        else if (color == "Blue")
        {
            if (hero.superEvolution <= 2) return frameBlue;
            else if (hero.superEvolution <= 4) return frameViolet;
            else if (hero.superEvolution <= 6) return frameOrange1;
            else if (hero.superEvolution <= 8) return frameOrange2;
            else if (hero.superEvolution <= 10) return frameOrange3;
            else if (hero.superEvolution <= 12) return frameRed1;
            else if (hero.superEvolution <= 14) return frameRed2;
            else if (hero.superEvolution <= 16) return frameRed3;
            else return frameYellow1;
        }
        else if (color == "Violet")
        {
            if (hero.superEvolution <= 2) return frameViolet;
            else if (hero.superEvolution <= 4) return frameOrange1;
            else if (hero.superEvolution <= 6) return frameOrange2;
            else if (hero.superEvolution <= 8) return frameOrange3;
            else if (hero.superEvolution <= 10) return frameRed1;
            else if (hero.superEvolution <= 12) return frameRed2;
            else if (hero.superEvolution <= 14) return frameRed2;
            else if (hero.superEvolution <= 16) return frameYellow1;
            else return frameYellow2;
        }
        else if (color == "Orange")
        {
            if (hero.superEvolution <= 2) return frameOrange1;
            else if (hero.superEvolution <= 4) return frameOrange2;
            else if (hero.superEvolution <= 6) return frameOrange3;
            else if (hero.superEvolution <= 8) return frameRed1;
            else if (hero.superEvolution <= 10) return frameRed2;
            else if (hero.superEvolution <= 12) return frameRed3;
            else if (hero.superEvolution <= 14) return frameYellow1;
            else if (hero.superEvolution <= 16) return frameYellow2;
            else return frameYellow3;
        }
        else if (color == "Red")
        {
            if (hero.superEvolution <= 4) return frameRed1;
            else if (hero.superEvolution <= 8) return frameRed2;
            else if (hero.superEvolution <= 14) return frameYellow1;
            else if (hero.superEvolution <= 16) return frameYellow2;
            else return frameYellow3;
        }
        else if (color == "Yellow")
        {
            if (hero.superEvolution <= 4) return frameYellow1;
            else if (hero.superEvolution <= 10) return frameYellow2;
            else return frameYellow3;
        }
        else return itemAssets.GetBackground();
    }
}
