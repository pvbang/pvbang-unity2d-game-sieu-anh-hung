using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase.Auth;
using Firebase.Database;
using System;
using Firebase.Extensions;

public class PhysicalController : MonoBehaviour
{
    private FirebaseUser user;
    private string ServerID;
    public TextMeshProUGUI physicalText;

    private DatabaseReference referenceGames;

    private void Awake()
    {
        user = FirebaseAuth.DefaultInstance.CurrentUser;
        ServerID = PlayerPrefs.GetString("ServerID");
        physicalText = transform.Find("Text").GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        InvokeRepeating("StartAddPhysical", 0, 300);

        if (user == null) return;
        if (ServerID == "") return;

        referenceGames = FirebaseDatabase.DefaultInstance.GetReference("accounts").Child(user.UserId).Child("servers").Child(ServerID).Child("games");
        //referenceGames.ValueChanged += HandleValueChangedGames;
    }


    // cập nhật thông tin Physical database
    public void UpdatePhysical(int physical)
    {
        referenceGames.Child("physical").SetValueAsync(physical);
    }

    // 
    public void CheckPhysical(int physical, int maxPhysical)
    {
        if (physical > maxPhysical)
        {
            referenceGames.Child("physical").SetValueAsync(maxPhysical);
            return;
        }
    }

    void StartAddPhysical()
    {
        StartCoroutine(AddPhysical());
    }

    public IEnumerator AddPhysical()
    {
        string physical = "";
        string maxPhysical = "";
        string timePhysical = "";

        referenceGames.GetValueAsync().ContinueWithOnMainThread(task => {
            if (task.IsFaulted)
            {
                Debug.Log("Error: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                physical = snapshot.Child("physical").Value.ToString();
                maxPhysical = snapshot.Child("maxPhysical").Value.ToString();
                timePhysical = snapshot.Child("timePhysical").Value.ToString();
            }
        });

        //referenceGames.Child("physical").GetValueAsync().ContinueWithOnMainThread(task => {
        //    if (task.IsFaulted)
        //    {
        //        Debug.Log("Error: " + task.Exception);
        //    }
        //    else if (task.IsCompleted)
        //    {
        //        DataSnapshot snapshot = task.Result;
        //        physical = snapshot.Value.ToString();
        //    }
        //});

        //referenceGames.Child("timePhysical").GetValueAsync().ContinueWithOnMainThread(task => {
        //    if (task.IsFaulted)
        //    {
        //        Debug.Log("Error: " + task.Exception);
        //    }
        //    else if (task.IsCompleted)
        //    {
        //        DataSnapshot snapshot = task.Result;
        //        timePhysical = snapshot.Value.ToString();
        //    }
        //});

        // chờ 3s
        yield return new WaitForSeconds(3);

        if (timePhysical == "")
        {
            timePhysical = DateTime.Now.ToString();
            referenceGames.Child("timePhysical").SetValueAsync(timePhysical);
            CalculatorPhysical(timePhysical, physical, maxPhysical);
        }
        else
        {
            CalculatorPhysical(timePhysical, physical, maxPhysical);
        }
    }


    private void CalculatorPhysical(string firstTime, string physical, string maxPhysical)
    {
        DateTime nowTime = DateTime.Now;
        if (physical == maxPhysical)
        {
            referenceGames.Child("timePhysical").SetValueAsync(nowTime.ToString());
            return;
        }


        DateTime firstDateTime = DateTime.Parse(firstTime);

        // Chuyển đổi DateTime thành TimeSpan và lấy giá trị giây
        TimeSpan timeSpan = nowTime - firstDateTime;
        double seconds = timeSpan.TotalSeconds;

        int time = (int)Math.Floor(seconds / 60);

        if (time >= 1)
        {
            int newPhysical = int.Parse(physical) + time;
            if (newPhysical > int.Parse(maxPhysical))
            {
                newPhysical = int.Parse(maxPhysical);
            }

            referenceGames.Child("physical").SetValueAsync(newPhysical);
            referenceGames.Child("timePhysical").SetValueAsync(nowTime.ToString());
        }
    }
}
