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

    public Sprite star;
    public Sprite moon;

    public GameObject Star;
    public GameObject Effect;

    public Image CS1;
    public Image CS2;
    public Image CS3;
    public Image CS4;
    public Image CS5;

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
        if (hero == null) return;

        GameObject heroObject = GameAssets.Instance.GetGameObjectFromId(hero.h_id);
        if (heroObject != null)
        {
            ItemAssets itemAssets = heroObject.GetComponent<ItemAssets>();
            if (itemAssets != null)
            {
                this.background.sprite = GetFrameBackground(itemAssets, hero);
                this.image.sprite = itemAssets.GetImage();
                SetStar(hero.reincarnation);

                if (hero.reincarnation >=3)
                {
                    this.Star.SetActive(true);
                } else
                {
                    this.Star.SetActive(false);
                }

                if (hero.reincarnation >= 5)
                {
                    this.Effect.SetActive(true);
                }
                else
                {
                    this.Effect.SetActive(false);
                }
            }
        }
        this.heroLevel.text = "Lv. " + hero.h_level.ToString() + " / " + hero.h_levelMax.ToString();
        this.heroName.text = hero.h_name;
    }

    // hiển thị khung siêu tiến hóa
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

    // hiển thị sao chuyển sinh
    public void SetStar(int star)
    {
        if (star == 0) SetStarSprite(null, null, null, null, null);
        else if(star == 1) SetStarSprite(this.star, null, null, null, null);
        else if (star == 2) SetStarSprite(this.star, this.star, null, null, null);
        else if (star == 3) SetStarSprite(this.star, this.star, this.star, null, null);
        else if (star == 4) SetStarSprite(this.star, this.star, this.star, this.star, null);
        else if (star == 5) SetStarSprite(this.star, this.star, this.star, this.star, this.star);

        else if (star == 6) SetStarSprite(this.moon, null, null, null, null);
        else SetStarSprite(this.moon, this.moon, null, null, null);
    }

    public void SetStarSprite(Sprite cs1, Sprite cs2, Sprite cs3, Sprite cs4, Sprite cs5)
    {
        if (cs1 == null) CS1.gameObject.SetActive(false);
        else CS1.sprite = cs1;

        if (cs2 == null) CS2.gameObject.SetActive(false);
        else CS2.sprite = cs2;

        if (cs3 == null) CS3.gameObject.SetActive(false);
        else CS3.sprite = cs3;

        if (cs4 == null) CS4.gameObject.SetActive(false);
        else CS4.sprite = cs4;

        if (cs5 == null) CS5.gameObject.SetActive(false);
        else CS5.sprite = cs5;
    }
}
