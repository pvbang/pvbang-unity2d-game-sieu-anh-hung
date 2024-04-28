using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemCS : MonoBehaviour
{
    public TextMeshProUGUI itemName;

    public Image itemColor;
    public Image itemFrame;
    public Image itemIcon;
    public TextMeshProUGUI itemCount;

    public TextMeshProUGUI itemCost;
    public Image itemCostImage;

    public void SetItem(ItemAssets itemAssets, string count)
    {
        itemName.text = itemAssets.GetItemName();
        itemColor.color = itemAssets.GetColorFromEnum(itemAssets.GetColor());
        itemFrame.sprite = itemAssets.GetFrame();
        itemIcon.sprite = itemAssets.GetIcon();
        itemCount.text = "x" + itemAssets.GetCount();

        itemCost.text = itemAssets?.GetCost() ?? "0";
        itemCostImage.sprite = itemAssets.GetCostIcon();
    }
}
