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

    public ItemAssets itemAssets;

    public void SetItem(ItemAssets itemAssets)
    {
        gameObject.name = itemAssets.GetID();
        itemName.text = itemAssets.GetItemName();
        itemColor.color = itemAssets.GetColorFromEnum(itemAssets.GetColor(), 1f);
        itemFrame.sprite = itemAssets.GetFrame();
        itemIcon.sprite = itemAssets.GetIcon();
        itemCount.text = "x" + itemAssets.GetCount();

        itemCost.text = itemAssets?.GetCost() ?? "0";
        itemCostImage.sprite = itemAssets.GetCostIcon();

        //
        this.itemAssets = itemAssets;
    }

    public ItemAssets GetItemAssets()
    {
        return itemAssets;
    }
}
