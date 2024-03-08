using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ListColor
{
    White,
    Gray,
    Green,
    Blue,
    Violet,
    Orange,
    Red,
    Yellow,
    Colorful
}

public class ItemAssets : MonoBehaviour
{
    public string itemName = "";
    public Sprite icon;
    public Sprite frame;
    public Sprite image;
    public Sprite background;

    public ListColor listColor;

    public string GetItemName()
    {
        return itemName;
    }

    public Sprite GetIcon()
    {
        return icon;
    }

    public Sprite GetFrame()
    {
        return frame;
    }

    public Sprite GetImage()
    {
        return image;
    }

    public Sprite GetBackground()
    {
        return background;
    }

    public string GetColor()
    {
        return listColor.ToString();
    }
}
