using Firebase.Auth;
using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class _Servers
{
    // tạo server mới
    public static IEnumerator CreateNewServer(string id, string username, string status)
    {
        var task = FirebaseConnection.instance.databaseReference.Child("servers").Child(id).GetValueAsync();

        yield return new WaitUntil(() => task.IsCompleted);

        if (task.IsFaulted)
        {
            NotificationGame.instance.ShowNotifications("Lỗi tạo server");
        }
        else if (task.IsCompleted)
        {
            DataSnapshot snapshot = task.Result;
            if (snapshot.Exists)
            {
                NotificationGame.instance.ShowNotifications("Tên server đã tồn tại");
            }
            else
            {
                Server server = new Server(id, username, status, 0);
                string json = JsonUtility.ToJson(server);

                FirebaseConnection.instance.databaseReference.Child("servers").Child(id).SetRawJsonValueAsync(json);

                NotificationGame.instance.ShowNotifications("Tạo server thành công");
            }
        }
    }

    // xóa server
    public static IEnumerator DeleteServer(string id)
    {
        var task = FirebaseConnection.instance.databaseReference.Child("servers").Child(id).GetValueAsync();

        yield return new WaitUntil(() => task.IsCompleted);

        if (task.IsFaulted)
        {
            NotificationGame.instance.ShowNotifications("Lỗi xóa server");
        }
        else if (task.IsCompleted)
        {
            DataSnapshot snapshot = task.Result;
            if (snapshot.Exists)
            {
                FirebaseConnection.instance.databaseReference.Child("servers").Child(id).RemoveValueAsync();

                NotificationGame.instance.ShowNotifications("Xóa server thành công");
            }
            else
            {
                NotificationGame.instance.ShowNotifications("Server không tồn tại");
            }
        }
    }


    // cập nhật thông tin server
    public static IEnumerator UpdateServer(string id, string username, string status, int quantity)
    {
        var task = FirebaseConnection.instance.databaseReference.Child("servers").Child(id).GetValueAsync();

        yield return new WaitUntil(() => task.IsCompleted);

        if (task.IsFaulted)
        {
            NotificationGame.instance.ShowNotifications("Lỗi cập nhật thông tin server");
        }
        else if (task.IsCompleted)
        {
            DataSnapshot snapshot = task.Result;
            if (snapshot.Exists)
            {
                Server server = new Server(id, username, status, quantity);
                string json = JsonUtility.ToJson(server);

                FirebaseConnection.instance.databaseReference.Child("servers").Child(id).SetRawJsonValueAsync(json);

                NotificationGame.instance.ShowNotifications("Cập nhật thông tin server thành công");
            }
            else
            {
                NotificationGame.instance.ShowNotifications("Server không tồn tại");
            }
        }
    }

    // cập nhật thông tin quantity của server
    public static void UpdateServerQuantity(string id, int quantity, Action<bool> callback)
    {
        var task = FirebaseConnection.instance.databaseReference.Child("servers").Child(id).GetValueAsync();

        task.ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("Lỗi cập nhật thông tin quantity server: " + task.Exception);
                callback(false);
                return;
            }

            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    Server server = JsonUtility.FromJson<Server>(snapshot.GetRawJsonValue());
                    server.quantity = quantity;
                    if (quantity >= 200) server.status = "HOT";
                    if (quantity >= 500) server.status = "FULL";

                    string json = JsonUtility.ToJson(server);

                    FirebaseConnection.instance.databaseReference.Child("servers").Child(id).SetRawJsonValueAsync(json);

                    Debug.Log("Cập nhật thông tin server thành công");
                    callback(true);
                }
                else
                {
                    Debug.Log("Server không tồn tại");
                    callback(false);
                }
            }
        });
    }

    // lấy quantity của server
    public static void GetServerQuantity(string id, Action<int> callback)
    {
        var task = FirebaseConnection.instance.databaseReference.Child("servers").Child(id).GetValueAsync();

        task.ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("Lỗi lấy thông tin quantity server: " + task.Exception);
                callback(-1);
                return;
            }

            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                if (snapshot.Exists)
                {
                    Server server = JsonUtility.FromJson<Server>(snapshot.GetRawJsonValue());
                    Debug.Log("Lấy thông tin quantity server thành công");
                    callback(server.quantity);
                }
                else
                {
                    Debug.Log("Server không tồn tại");
                    callback(-1);
                }
            }
        });
    }
}
