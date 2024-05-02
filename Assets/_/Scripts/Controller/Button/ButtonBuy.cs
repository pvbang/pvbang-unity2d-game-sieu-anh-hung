using Firebase.Auth;
using Firebase.Database;
using Google.MiniJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class ButtonBuy : BaseButton
{
    public ItemCS itemCS;

    private void Awake()
    {
        if (itemCS == null)
        {
            itemCS = GetComponentInParent<ItemCS>();
        }
    }

    protected override void OnClick()
    {
        StartCoroutine(GetItemInfoById(itemCS.GetItemAssets().GetCostObjectIDName()));
    }


    public IEnumerator GetItemInfoById(string itemID)
    {
        var itemRef = FirebaseConnection.instance.databaseReference
            .Child("accounts")
            .Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId)
            .Child("servers")
            .Child(PlayerPrefs.GetString("ServerID"))
            .Child("items")
            .Child(itemID);

        var getTask = itemRef.GetValueAsync();

        // Đợi cho đến khi hoàn thành việc lấy dữ liệu
        yield return new WaitUntil(() => getTask.IsCompleted);

        if (getTask.IsFaulted)
        {
            Debug.Log("Lỗi khi lấy dữ liệu item");
        }
        else if (getTask.IsCompleted)
        {
            DataSnapshot snapshot = getTask.Result;
            int currentValue = snapshot.Exists ? Convert.ToInt32(snapshot.Value) : 0;

            if (currentValue - int.Parse(itemCS.GetItemAssets().GetCost()) >= 0)
            {
                UpdateValue();
            } 
            else
            {
                NotificationGame.instance.ShowNotifications("Không đủ " + itemCS.GetItemAssets().GetCostObjectName());
            }
        }
    }

    public void UpdateValue()
    {
        string itemID = this.transform.parent.name.Replace("(Clone)", "");

        ItemAssets itemAttribute = itemCS.GetItemAssets();

        StartCoroutine(ItemManager.CountToAddItem(itemID, +itemAttribute.GetCount()));

        ShowReward.instance.ShowRewardNotification(itemAttribute.GetIcon(), itemAttribute.GetFrame(), itemAttribute.GetItemName() + " x" + itemAttribute.GetCount());
        StartCoroutine(ItemManager.CountToAddItem(itemAttribute.GetCostObjectIDName(), -int.Parse(itemAttribute.GetCost())));
    }

}
