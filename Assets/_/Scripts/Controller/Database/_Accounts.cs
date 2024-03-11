using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase.Auth;
using UnityEngine.SceneManagement;
using Firebase.Database;
using System;
using UnityEngine.UIElements;
using Firebase.Extensions;

public static class _Accounts
{
    // thêm nhân vật mới vào account
    public static IEnumerator AddAccount(string ServerID, string _id, string _name, int avatarIndex, bool loadMain)
    {
        WaitingController.Instance.StartWaiting();
        DatabaseReference reference = FirebaseConnection.instance.databaseReference.Child("accounts").Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId).Child("servers").Child(ServerID);

        int quantity = -2;
        _Servers.GetServerQuantity(ServerID, _quantity =>
        {
            quantity = _quantity;
        });

        while (quantity == -2)
        {
            yield return new WaitForSeconds(0.1f);
        }

        Dictionary<string, object> gamesData = new Dictionary<string, object>
        {
            { "id", _id },
            { "name", _name },
            { "level", 0 },
            { "exp", 0 },
            { "maxEXP", 100 },
            { "power", 0 },
            { "physical", 120 },
            { "maxPhysical", 120 },
            { "vip", 0 },
            { "avatar", avatarIndex },
            { "rank", quantity + 1 },
            { "lastLogin", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") },
            { "created", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") },
        };

        Dictionary<string, object> itemsData = new Dictionary<string, object>
        {
            { "diamond", 0 },
            { "meat", 0 },
            { "iron", 0 }
        };

        var task = reference.Child("games").UpdateChildrenAsync(gamesData);
        var task2 = reference.Child("items").UpdateChildrenAsync(itemsData);

        //__Account __account = new __Account();
        //__account.UpdateServerQuantity(ServerID, quantity + 1);

        yield return new WaitUntil(() => task.IsCompleted);
        yield return new WaitUntil(() => task2.IsCompleted);

        if (task.IsFaulted)
        {
            Debug.Log("Lỗi thêm nhân vật");
        }
        else if (task.IsCompleted)
        {
            if (loadMain) SceneManager.LoadScene("MainScene");
        }

        WaitingController.Instance.EndWaiting();
    }
}

public class __Account : MonoBehaviour
{
    public void UpdateServerQuantity(string ServerID, int quantity)
    {
        StartCoroutine(_Servers.UpdateServerQuantity(ServerID, quantity));
    }
}
