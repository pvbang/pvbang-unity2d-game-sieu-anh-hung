using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemAttribute : MonoBehaviour
{
    public string iconName = "";
    public GameObject image;
    public GameObject frame;

    private void Awake()
    {
        if (image == null)
        {
            image = transform.Find("Item").Find("Image").gameObject;
        }
        if (frame == null)
        {
            frame = transform.Find("Item").Find("Image").Find("Frame").gameObject;
        }
    }

    public string GetIconName()
    {
        return iconName;
    }

    public Image GetImage()
    {
        return image.GetComponent<Image>();
    }

    public Image GetFrame()
    {
        return frame.GetComponent<Image>();
    }
}
