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
        user = FirebaseConnection.instance.auth.CurrentUser;
        ServerID = PlayerPrefs.GetString("ServerID");
        physicalText = transform.Find("Text").GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        InvokeRepeating("StartAddPhysical", 0, 60);

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
        if (physical >= maxPhysical)
        {
            referenceGames.Child("physical").SetValueAsync(maxPhysical);
            return;
        }
        else 
        { 
            
        }
    }

    void StartAddPhysical()
    {
        StartCoroutine(AddPhysical());
    }

    public IEnumerator AddPhysical()
    {
        string physical = "";
        string timePhysical = "";

        referenceGames.Child("physical").GetValueAsync().ContinueWithOnMainThread(task => {
            if (task.IsFaulted)
            {
                Debug.Log("Error: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                physical = snapshot.Value.ToString();
            }
        });

        referenceGames.Child("timePhysical").GetValueAsync().ContinueWithOnMainThread(task => {
            if (task.IsFaulted)
            {
                Debug.Log("Error: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                timePhysical = snapshot.Value.ToString();
            }
        });

        // chờ 3s
        yield return new WaitForSeconds(3);

        if (timePhysical == "")
        {
            timePhysical = DateTime.Now.ToString();
            referenceGames.Child("timePhysical").SetValueAsync(timePhysical);
            CalculatorPhysical(timePhysical, physical);
        }
        else
        {
            CalculatorPhysical(timePhysical, physical);
        }
    }


    private void CalculatorPhysical(string firstTime, string physical)
    {
        DateTime nowTime = DateTime.Now;
        DateTime firstDateTime = DateTime.Parse(firstTime);

        // Chuyển đổi DateTime thành TimeSpan và lấy giá trị giây
        TimeSpan timeSpan = nowTime - firstDateTime;
        double seconds = timeSpan.TotalSeconds;

        int time = (int)Math.Floor(seconds / 60);

        if (time >= 1)
        {
            referenceGames.Child("physical").SetValueAsync(int.Parse(physical) + time);
            referenceGames.Child("timePhysical").SetValueAsync(nowTime.ToString());
        }
    }
}
