using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

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
    public string costObjectIDName;
    public int count = 1;


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

    public int GetCount()
    {
        return count;
    }

    public string GetCostObjectIDName()
    {
        return costObjectIDName;
    }

    public GameObject GetCostObject()
    {
        GameObject itemObject = GameAssets.Instance.GetNguyenLieuChuyenSinhFromId(costObjectIDName);
        return itemObject;
    }

    public string GetCostObjectName()
    {
        GameObject itemObject = GameAssets.Instance.GetNguyenLieuChuyenSinhFromId(costObjectIDName);

        ItemAssets itemAssets = itemObject.GetComponent<ItemAssets>();
        return itemAssets.GetItemName();
    }

    public Sprite GetCostIcon()
    {
        GameObject itemObject = GetCostObject();
        if (itemObject != null)
        {
            return itemObject.GetComponent<ItemAssets>().GetIcon();
        }
        return null;
    }

    public string GetID()
    {
        return gameObject.name;
    }


    public Color32 GetColorFromEnum(string color, float brightnessFactor)
    {
        Color32 baseColor;
        switch (color)
        {
            case "White":
                baseColor = new Color32(255, 255, 255, 255);
                break;
            case "Gray":
                baseColor = new Color32(128, 128, 128, 255);
                break;
            case "Green":
                baseColor = new Color32(100, 185, 0, 255);
                break;
            case "Blue":
                baseColor = new Color32(45, 80, 204, 255);
                break;
            case "Violet":
                baseColor = new Color32(230, 0, 230, 255); 
                break;
            case "Orange":
                baseColor = new Color32(230, 100, 0, 255);
                break;
            case "Red":
                baseColor = new Color32(238, 17, 17, 255);
                break;
            case "Yellow":
                baseColor = new Color32(255, 222, 0, 255);
                break;
            case "Colorful":
                baseColor = new Color32((byte)Random.Range(0, 256), (byte)Random.Range(0, 256), (byte)Random.Range(0, 256), 255);
                break;
            default:
                baseColor = new Color32(0, 0, 0, 255);
                break;
        }

        byte r = (byte)(baseColor.r * brightnessFactor);
        byte g = (byte)(baseColor.g * brightnessFactor);
        byte b = (byte)(baseColor.b * brightnessFactor);

        return new Color32(r, g, b, baseColor.a);
    }

}
