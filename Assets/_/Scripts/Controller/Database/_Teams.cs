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

public static class _Teams
{
    // thêm hero vào vị trí trong team
    public static IEnumerator AddHeroToPosition(string TeamID, string Position, string HeroID)
    {
        WaitingController.Instance.StartWaiting();
        var task = FirebaseConnection.instance.databaseReference.Child("accounts").Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId).Child("servers").Child(PlayerPrefs.GetString("ServerID")).Child("teams").Child(TeamID).Child(Position).SetValueAsync(HeroID);

        yield return new WaitUntil(() => task.IsCompleted);

        if (task.IsFaulted)
        {
            Debug.Log("Lỗi set vị trí hero");
        }
        else if (task.IsCompleted)
        {
            SceneManager.LoadScene(PlayerPrefs.GetString("LastScene"));
        }

        WaitingController.Instance.EndWaiting();
    }

    // lấy thông tin team
    public static void GetTeam(string TeamID, Action<Position> callback)
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

        var task = FirebaseConnection.instance.databaseReference.Child("accounts").Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId).Child("servers").Child(PlayerPrefs.GetString("ServerID")).Child("teams").Child(TeamID).GetValueAsync();

        task.ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("Lỗi lấy thông tin team: " + task.Exception);
                callback(null);
                return;
            }

            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                if (snapshot.Exists)
                {
                    string position_1 = (snapshot.Child("position_1").Exists) ? snapshot.Child("position_1").Value.ToString() : null;
                    string position_2 = (snapshot.Child("position_2").Exists) ? snapshot.Child("position_2").Value.ToString() : null;
                    string position_3 = (snapshot.Child("position_3").Exists) ? snapshot.Child("position_3").Value.ToString() : null;
                    string position_4 = (snapshot.Child("position_4").Exists) ? snapshot.Child("position_4").Value.ToString() : null;
                    string position_5 = (snapshot.Child("position_5").Exists) ? snapshot.Child("position_5").Value.ToString() : null;
                    string position_6 = (snapshot.Child("position_6").Exists) ? snapshot.Child("position_6").Value.ToString() : null;
                    string position_7 = (snapshot.Child("position_7").Exists) ? snapshot.Child("position_7").Value.ToString() : null;
                    string position_8 = (snapshot.Child("position_8").Exists) ? snapshot.Child("position_8").Value.ToString() : null;
                    string position_9 = (snapshot.Child("position_9").Exists) ? snapshot.Child("position_9").Value.ToString() : null;

                    Position position = new Position(position_1, position_2, position_3, position_4, position_5, position_6, position_7, position_8, position_9);

                    callback(position);
                }
                else
                {
                    Debug.Log("Không tìm thấy team");
                    callback(null);
                }
            }
        });
    }

    // lấy thông tin team từ position
    public static void GetTeamFromPosition(string TeamID, string Position, Action<string> callback)
    {
        if (FirebaseAuth.DefaultInstance.CurrentUser == null)
        {
            callback("");
            return;
        }
        if (PlayerPrefs.GetString("ServerID") == "")
        {
            callback("");
            return;
        }

        FirebaseConnection.instance.databaseReference.Child("accounts").Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId).Child("servers").Child(PlayerPrefs.GetString("ServerID")).Child("teams").Child(TeamID).GetValueAsync().ContinueWithOnMainThread(task => {
            Debug.Log("Lấy thông tin team từ position: " + Position);
            if (task.IsFaulted)
            {
                Debug.Log("Lỗi lấy thông tin team từ position: " + task.Exception);
                callback("");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                if (snapshot.Exists)
                {
                    if (snapshot.Child(Position).Exists)
                    {
                        string heroID = snapshot.Child(Position).Value.ToString();

                        callback(heroID);
                    }
                    else
                    {
                        Debug.Log("Không tìm thấy hero");
                        callback("");
                    }
                }
                else
                {
                    Debug.Log("Không tìm thấy hero");
                    callback("");
                }
            }
            else
            {
                Debug.Log("Task không hoàn thành");
                callback("");
            }
        });
    }

    // kiểm tra hero có trong team không
    public static void CheckHeroInTeam(string TeamID, string HeroID, Action<bool> callback)
    {
        if (FirebaseAuth.DefaultInstance.CurrentUser == null)
        {
            callback(false);
            return;
        }
        if (PlayerPrefs.GetString("ServerID") == "")
        {
            callback(false);
            return;
        }

        FirebaseConnection.instance.databaseReference.Child("accounts").Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId).Child("servers").Child(PlayerPrefs.GetString("ServerID")).Child("teams").Child(TeamID).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("Lỗi kiểm tra hero có trong team không: " + task.Exception);
                callback(false);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                if (snapshot.Exists)
                {
                    if (snapshot.Child("position_1").Exists && snapshot.Child("position_1").Value.ToString() == HeroID)
                    {
                        callback(true);
                    }
                    else if (snapshot.Child("position_2").Exists && snapshot.Child("position_2").Value.ToString() == HeroID)
                    {
                        callback(true);
                    }
                    else if (snapshot.Child("position_3").Exists && snapshot.Child("position_3").Value.ToString() == HeroID)
                    {
                        callback(true);
                    }
                    else if (snapshot.Child("position_4").Exists && snapshot.Child("position_4").Value.ToString() == HeroID)
                    {
                        callback(true);
                    }
                    else if (snapshot.Child("position_5").Exists && snapshot.Child("position_5").Value.ToString() == HeroID)
                    {
                        callback(true);
                    }
                    else if (snapshot.Child("position_6").Exists && snapshot.Child("position_6").Value.ToString() == HeroID)
                    {
                        callback(true);
                    }
                    else if (snapshot.Child("position_7").Exists && snapshot.Child("position_7").Value.ToString() == HeroID)
                    {
                        callback(true);
                    }
                    else if (snapshot.Child("position_8").Exists && snapshot.Child("position_8").Value.ToString() == HeroID)
                    {
                        callback(true);
                    }
                    else if (snapshot.Child("position_9").Exists && snapshot.Child("position_9").Value.ToString() == HeroID)
                    {
                        callback(true);
                    }
                    else
                    {
                        callback(false);
                    }
                }
                else
                {
                    Debug.Log("Không tìm thấy team");
                    callback(false);
                }
            }
        });
    }
}
