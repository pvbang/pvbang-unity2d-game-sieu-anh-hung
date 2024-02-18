using Firebase.Auth;
using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HeroManager
{
    // thêm hero mới vào account
    public static IEnumerator AddNewHero(HeroUnit hero)
    {
        string heroID = "HERO_" + RandomStringGenerator.GenerateRandomString(20);
        hero.id = heroID;
        string json = JsonUtility.ToJson(hero);
        var task = FirebaseConnection.instance.databaseReference.Child("accounts").Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId).Child("servers").Child(PlayerPrefs.GetString("ServerID")).Child("heros").Child(heroID).SetRawJsonValueAsync(json);

        yield return new WaitUntil(() => task.IsCompleted);

        if (task.IsFaulted)
        {
            Debug.Log("Lỗi thêm hero");
        }
        else if (task.IsCompleted)
        {
            Debug.Log("Thêm hero thành công");
        }
    }

    // lấy danh sách hero của account
    public static void GetListHeroByAccount(Action<List<Hero>> callback)
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

        var task = FirebaseConnection.instance.databaseReference.Child("accounts").Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId).Child("servers").Child(PlayerPrefs.GetString("ServerID")).Child("heros").GetValueAsync();

        task.ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("Lỗi lấy danh sách hero của account: " + task.Exception);
                callback(null);
                return;
            }

            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                if (snapshot.Exists)
                {
                    List<Hero> heros = new List<Hero>();
                    foreach (DataSnapshot child in snapshot.Children)
                    {
                        heros.Add(JsonUtility.FromJson<Hero>(child.GetRawJsonValue()));
                    }

                    Debug.Log("Lấy danh sách hero của account thành công");
                    callback(heros);
                }
                else
                {
                    callback(null);
                }
            }
        });
    }
}
