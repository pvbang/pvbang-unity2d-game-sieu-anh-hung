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

    public string costObjectIDName = "";
    public string cost = "0";

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
        costObjectIDName = itemAssets.GetCostObjectIDName();
        cost = itemAssets.GetCost();
    }

    public string GetCostObjectIDName()
    {
        return costObjectIDName;
    }

    public string GetCost()
    {
        return cost;
    }
}
