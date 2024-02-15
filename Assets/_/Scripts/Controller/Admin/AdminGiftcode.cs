using Firebase.Auth;
using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdminGiftcode : MonoBehaviour
{
    private FirebaseUser user;

    public string giftcodeID;
    public string[] itemID;
    public string[] quantity;

    private void Awake()
    {
        user = FirebaseAuth.DefaultInstance.CurrentUser;
    }

    // Tạo giftcode mới
    public void CreateNewGiftcodeButton()
    {
        if (user == null) return;
        if (giftcodeID == "") return;
        if (itemID.Length == 0) return;
        if (quantity.Length == 0) return;

        StartCoroutine(CreateNewGiftcode(giftcodeID, itemID, quantity));
    }

    IEnumerator CreateNewGiftcode(string giftcodeID, string[] itemID, string[] quantity)
    {
        var task = FirebaseConnection.instance.databaseReference.Child("giftcodes").Child(giftcodeID).GetValueAsync();

        yield return new WaitUntil(() => task.IsCompleted);

        if (task.IsFaulted)
        {
            Notification.instance.ShowNotifications("Lỗi tạo giftcode");
        }
        else if (task.Result.Exists)
        {
            Notification.instance.ShowNotifications("Giftcode đã tồn tại");
        }
        else
        {
            Dictionary<string, object> dataItems = new Dictionary<string, object>();

            // Duyệt qua mảng itemID và quantity để tạo một dictionary mới cho items
            for (int i = 0; i < itemID.Length && i < quantity.Length; i++)
            {
                dataItems.Add(itemID[i], quantity[i]);
            }

            Dictionary<string, object> data = new Dictionary<string, object>
            {
                { "items", dataItems },
                { "timestamp", DateTime.Now.ToString() }
            };

            FirebaseConnection.instance.databaseReference.Child("giftcodes").Child(giftcodeID).SetValueAsync(data);

            Notification.instance.ShowNotifications("Tạo giftcode thành công");
        }
    }


    // Chỉnh sửa giftcode
    public void EditGiftcodeButton()
    {
        if (user == null) return;
        if (giftcodeID == "") return;
        if (itemID.Length == 0) return;
        if (quantity.Length == 0) return;

        StartCoroutine(EditGiftcode(giftcodeID, itemID, quantity));
    }

    IEnumerator EditGiftcode(string giftcodeID, string[] itemID, string[] quantity)
    {
        var task = FirebaseConnection.instance.databaseReference.Child("giftcodes").Child(giftcodeID).GetValueAsync();

        yield return new WaitUntil(() => task.IsCompleted);

        if (task.IsFaulted)
        {
            Notification.instance.ShowNotifications("Lỗi chỉnh sửa giftcode");
        }
        else if (!task.Result.Exists)
        {
            Notification.instance.ShowNotifications("Giftcode không tồn tại");
        }
        else
        {
            Dictionary<string, object> dataItems = new Dictionary<string, object>();

            // Duyệt qua mảng itemID và quantity để tạo một dictionary mới cho items
            for (int i = 0; i < itemID.Length && i < quantity.Length; i++)
            {
                dataItems.Add(itemID[i], quantity[i]);
            }

            Dictionary<string, object> data = new Dictionary<string, object>
            {
                { "items", dataItems },
                { "timestamp", DateTime.Now.ToString() }
            };

            FirebaseConnection.instance.databaseReference.Child("giftcodes").Child(giftcodeID).SetValueAsync(data);

            Notification.instance.ShowNotifications("Chỉnh sửa giftcode thành công");
        }
    }


    // Xóa giftcode
    public void DeleteGiftcodeButton()
    {
        if (user == null) return;
        if (giftcodeID == "") return;

        StartCoroutine(DeleteGiftcode(giftcodeID));
    }

    IEnumerator DeleteGiftcode(string giftcodeID)
    {
        var task = FirebaseConnection.instance.databaseReference.Child("giftcodes").Child(giftcodeID).GetValueAsync();

        yield return new WaitUntil(() => task.IsCompleted);

        if (task.IsFaulted)
        {
            Notification.instance.ShowNotifications("Lỗi xóa giftcode");
        }
        else if (!task.Result.Exists)
        {
            Notification.instance.ShowNotifications("Giftcode không tồn tại");
        }
        else
        {
            FirebaseConnection.instance.databaseReference.Child("giftcodes").Child(giftcodeID).RemoveValueAsync();
            Notification.instance.ShowNotifications("Xóa giftcode thành công");
        }
    }
}
