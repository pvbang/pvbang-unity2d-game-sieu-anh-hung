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

    public string cost;
    public Sprite costSprite;

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

    public string GetCost()
    {
        return cost;
    }

    public Sprite GetCostSprite()
    {
        return costSprite;
    }


    public Color32 GetColorFromEnum(string color)
    {
        switch (color)
        {
            case "White":
                return new Color32(255, 255, 255, 255);
            case "Gray":
                return new Color32(128, 128, 128, 255);
            case "Green":
                return new Color32(0, 255, 0, 255);
            case "Blue":
                return new Color32(0, 0, 255, 255);
            case "Violet":
                return new Color32(238, 130, 238, 255);
            case "Orange":
                return new Color32(255, 165, 0, 255);
            case "Red":
                return new Color32(255, 0, 0, 255);
            case "Yellow":
                return new Color32(255, 255, 0, 255);
            case "Colorful":
                return new Color32((byte)Random.Range(0, 256), (byte)Random.Range(0, 256), (byte)Random.Range(0, 256), 255);
            default:
                return new Color32(0, 0, 0, 255); 
        }
    }
}
