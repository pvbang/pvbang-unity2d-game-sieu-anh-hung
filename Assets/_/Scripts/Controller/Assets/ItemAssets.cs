using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemAssets : MonoBehaviour
{
    public string iconName = "";
    public Sprite icon;
    public Sprite frame;
    public Sprite image;
    public Sprite background;

    public string GetIconName()
    {
        return iconName;
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
}
