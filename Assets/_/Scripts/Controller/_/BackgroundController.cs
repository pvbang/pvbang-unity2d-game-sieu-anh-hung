using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundController : MonoBehaviour
{
    public bool isRandom = false;
    public bool isBackgroundDayNight = false;
    public int indexImage = 0;
    public Sprite[] sprites;

    private Image background;

    private void Awake()
    {
        background = GetComponent<Image>();
    }

    void Start()
    {
        if (isRandom)
        {
            RandomBackground();
        }

        if (isBackgroundDayNight)
        {
            if (System.DateTime.Now.Hour >= 6 && System.DateTime.Now.Hour <= 18)
            {
                ChangeBackgroundDayNight(true);
            }
            else
            {
                ChangeBackgroundDayNight(false);
            }
        }
    }

    // ramdom background
    public void RandomBackground()
    {
        indexImage = Random.Range(0, Mathf.Min(28, sprites.Length));

        if (indexImage >= 0 && indexImage < sprites.Length)
        {
            background.sprite = sprites[indexImage];
        }
        else
        {
            Debug.LogError("Err sprite: " + indexImage);
        }
    }

    // thay đổi background
    public void ChangeBackground(int indexImage)
    {
        if (indexImage >= 0 && indexImage < sprites.Length)
        {
            background.sprite = sprites[indexImage];
        }
        else
        {
            Debug.LogError("Err sprite: " + indexImage);
        }
    }


    // thay đổi background theo ngày và đêm
    public void ChangeBackgroundDayNight(bool isDay)
    {
        if (isDay)
        {
            ChangeBackground(0);
        }
        else
        {
            ChangeBackground(1);
        }
    }
}
