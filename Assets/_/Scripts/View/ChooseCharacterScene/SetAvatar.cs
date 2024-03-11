using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Firebase.Auth;

public class SetAvatar : BaseButton
{
    private string ServerID;
    private int AvatarIndex = 0;

    public TMP_InputField NameInput;
    public GameObject Char1;
    public GameObject Char2;
    public GameObject Char3;
    public GameObject Char4;

    private void Awake()
    {
        ServerID = PlayerPrefs.GetString("ServerID");

        // SetRandomName();
        GetAvatarIndex();
    }

    // tạo tên ngẫu nhiên cho nhân vật
    public void SetRandomName()
    {
        string randomName = RandomStringGenerator.GenerateRandomName();
        NameInput.text = randomName;
    }

    // lấy index của avatar (avatar đang được chọn)
    public int GetAvatarIndex()
    {
        if (Char1.activeSelf)
        {
            AvatarIndex = 1;
        }
        else if (Char2.activeSelf)
        {
            AvatarIndex = 2;
        }
        else if (Char3.activeSelf)
        {
            AvatarIndex = 3;
        }
        else if (Char4.activeSelf)
        {
            AvatarIndex = 4;
        }

        return AvatarIndex;
    }

    // khởi tạo và set thông tin cho account
    public void SetGameDataAsync()
    {
        if (FirebaseAuth.DefaultInstance.CurrentUser == null)
        {
            NotificationGame.instance.ShowNotifications("Bạn chưa đăng nhập");
            return;
        }

        if (NameInput.text.Length == 0)
        {
            NotificationGame.instance.ShowNotifications("Bạn chưa đặt tên cho nhân vật");
            return;
        }

        string name = NameInput.text;

        // kiểm tra tên nhân vật có trống không
        if (name.Length == 0)
        {
            NotificationGame.instance.ShowNotifications("Bạn chưa đặt tên cho nhân vật");
            return;
        }

        string userID = ServerID + "SAH" + RandomStringGenerator.GenerateRandomString(7);

        StartCoroutine(_Accounts.AddAccount(ServerID, userID, name, GetAvatarIndex(), true));
        _Inter_Servers.CreateNewInterServer(ServerID, userID);
    }

    protected override void OnClick()
    {
        SetGameDataAsync();
    }
}
