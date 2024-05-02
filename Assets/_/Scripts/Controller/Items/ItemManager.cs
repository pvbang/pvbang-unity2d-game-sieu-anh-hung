using Firebase.Auth;
using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ItemManager
{
    // thêm item mới vào account
    public static IEnumerator AddNewItem(string itemID, int count)
    {
        var task = FirebaseConnection.instance.databaseReference.Child("accounts").Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId).Child("servers").Child(PlayerPrefs.GetString("ServerID")).Child("items").Child(itemID).SetValueAsync(count);

        yield return new WaitUntil(() => task.IsCompleted);

        if (task.IsFaulted)
        {
            Debug.Log("Lỗi thêm item");
        }
        else if (task.IsCompleted)
        {
            Debug.Log("Thêm item thành công");
        }
    }

    // lấy danh sách item của account
    public static void GetListItemByAccount(Action<List<Item>> callback)
    {
        if (FirebaseAuth.DefaultInstance.CurrentUser == null)
        {
            callback(null);
            return;
        }
        if (PlayerPrefs.GetString("ServerID") == "")
        {
            callback(null);
            return;
        }

        var task = FirebaseConnection.instance.databaseReference.Child("accounts").Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId).Child("servers").Child(PlayerPrefs.GetString("ServerID")).Child("items").GetValueAsync();

        task.ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("Lỗi lấy danh sách item của account: " + task.Exception);
                callback(null);
                return;
            }

            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                if (snapshot.Exists)
                {
                    List<Item> items = new List<Item>();
                    foreach (DataSnapshot child in snapshot.Children)
                    {
                        items.Add(JsonUtility.FromJson<Item>(child.GetRawJsonValue()));
                    }

                    Debug.Log("Lấy danh sách items của account thành công");
                    callback(items);
                }
                else
                {
                    callback(null);
                }
            }
        });
    }

    // lấy thông tin item từ id
    //public static void GetItemInfoById(string itemID, Action<int> callback)
    //{
    //    if (FirebaseAuth.DefaultInstance.CurrentUser == null)
    //    {
    //        callback(0);
    //        return;
    //    }
    //    if (PlayerPrefs.GetString("ServerID") == "")
    //    {
    //        callback(0);
    //        return;
    //    }

    //    var task = FirebaseConnection.instance.databaseReference.Child("accounts").Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId).Child("servers").Child(PlayerPrefs.GetString("ServerID")).Child("item").Child(itemID).GetValueAsync();

    //    task.ContinueWith(task =>
    //    {
    //        if (task.IsFaulted)
    //        {
    //            Debug.Log("Lỗi lấy thông tin item từ id: " + task.Exception);
    //            callback(0);
    //            return;
    //        }

    //        if (task.IsCompleted)
    //        {
    //            DataSnapshot snapshot = task.Result;
    //            int currentValue = snapshot.Exists ? Convert.ToInt32(snapshot.Value) : 0;
    //            Debug.Log(currentValue);

    //            if (snapshot.Exists)
    //            {
    //                Debug.Log("??>>");
    //                //var item = snapshot.Value;
    //                //callback((int)item);
    //            }
    //            else
    //            {
    //                Debug.Log("Không tìm thấy item từ id");
    //                callback(0);
    //            }
    //        }
    //    });
    //}


    public static IEnumerator GetItemInfoById(string itemID)
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
            Debug.Log(currentValue);
        }
    }

    // cập nhật số lượng item
    public static IEnumerator CountToAddItem(string itemID, int countToAdd)
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
            int newValue = currentValue + countToAdd;

            // Cập nhật giá trị mới
            var setTask = itemRef.SetValueAsync(newValue);
            yield return new WaitUntil(() => setTask.IsCompleted);

            if (setTask.IsFaulted)
            {
                Debug.Log("Lỗi cập nhật item");
            }
            else if (setTask.IsCompleted)
            {
                Debug.Log("Cập nhật item thành công");
            }
        }
    }


}
