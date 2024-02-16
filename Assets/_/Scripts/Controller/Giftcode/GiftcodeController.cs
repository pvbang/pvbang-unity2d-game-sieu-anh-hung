using Firebase.Auth;
using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftcodeController : MonoBehaviour
{
    private FirebaseUser user;

    public bool isCreate = false;
    public bool isEdit = false;
    public bool isDelete = false;

    public string giftcodeID;
    public string[] itemID;
    public string[] quantity;
    public int timeEnd = 1;

    private void Awake()
    {
        user = FirebaseAuth.DefaultInstance.CurrentUser;
    }

    public void ForButton()
    {
        if (isCreate)
        {
            CreateNewGiftcodeButton();
        }
        else if (isEdit)
        {
            EditGiftcodeButton();
        }
        else if (isDelete)
        {
            DeleteGiftcodeButton();
        }
    }


    // Tạo giftcode mới
    public void CreateNewGiftcodeButton()
    {
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
                { "timestamp", DateTime.Now.AddDays(timeEnd).ToString() }
            };

            FirebaseConnection.instance.databaseReference.Child("giftcodes").Child(giftcodeID).SetValueAsync(data);

            Notification.instance.ShowNotifications("Tạo giftcode thành công");
        }
    }


    // Chỉnh sửa giftcode
    public void EditGiftcodeButton()
    {
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
                { "timestamp", DateTime.Now.AddDays(timeEnd).ToString() }
            };

            FirebaseConnection.instance.databaseReference.Child("giftcodes").Child(giftcodeID).SetValueAsync(data);

            Notification.instance.ShowNotifications("Chỉnh sửa giftcode thành công");
        }
    }


    // Xóa giftcode
    public void DeleteGiftcodeButton()
    {
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

    // lấy thông tin giftcode
    public void GetGiftcode(string giftcodeID, Action<Dictionary<string, object>> callback)
    {
        giftcodeID = giftcodeID.ToUpper();
        var task = FirebaseConnection.instance.databaseReference.Child("giftcodes").Child(giftcodeID).GetValueAsync();

        task.ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("Lỗi lấy thông tin giftcode: " + task.Exception);
                callback(null);
                return;
            }

            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                if (snapshot.Exists)
                {
                    Dictionary<string, object> data = task.Result.Value as Dictionary<string, object>;
                    callback(data);
                }
                else
                {
                    Debug.Log("Giftcode này không tồn tại");
                    callback(null);
                }
            }
        });
    }

    // kiểm tra thời gian sử dụng giftcode
    public bool CheckGiftcodeTime(string time)
    {
        DateTime end = DateTime.Parse(time);

        if (DateTime.Now <= end)
        {
            return true;
        }

        return false;
    }

    // thêm giftcode đã sử dụng vào user
    public void AddGiftcodeToUser(string giftcodeID, Dictionary<string, object> data, Action<bool> callback)
    {
        if (user == null)
        {
            callback(false);
            return;
        }

        giftcodeID = giftcodeID.ToUpper();

        FirebaseConnection.instance.databaseReference.Child("accounts").Child(user.UserId).Child("servers").Child(PlayerPrefs.GetString("ServerID")).Child("giftcodes").Child(giftcodeID).SetValueAsync("used").ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("Lỗi thêm giftcode vào user: " + task.Exception);
                callback(false);
                return;
            }

            if (task.IsCompleted)
            {
                callback(true);
            }
        });
    }

    // kiểm tra giftcode đã được sử dụng bởi user chưa
    public void CheckGiftcodeUsed(string giftcodeID, Action<bool> callback)
    {
        if (user == null)
        {
            callback(false);
            return;
        }

        giftcodeID = giftcodeID.ToUpper();
        var task = FirebaseConnection.instance.databaseReference.Child("accounts").Child(user.UserId).Child("servers").Child(PlayerPrefs.GetString("ServerID")).Child("giftcodes").Child(giftcodeID).GetValueAsync();

        task.ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("Lỗi kiểm tra giftcode đã sử dụng: " + task.Exception);
                callback(false);
                return;
            }

            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                if (snapshot.Exists)
                {
                    callback(true);
                }
                else
                {
                    callback(false);
                }
            }
        });
    }

}
