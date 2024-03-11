using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class _Inter_Servers
{
    // tạo inter server mới
    public static void CreateNewInterServer(string serverID, string userID)
    {
        var task = FirebaseConnection.instance.databaseReference.Child("inter_servers").Child("servers").Child(serverID).Child("accounts").SetValueAsync(userID);

        if (task.IsFaulted)
        {
            Debug.Log("Lỗi tạo inter server");
        }
    }
}
