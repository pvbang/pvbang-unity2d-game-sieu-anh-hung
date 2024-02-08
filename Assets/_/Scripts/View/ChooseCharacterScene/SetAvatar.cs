using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Firebase.Auth;

public class SetAvatar : BaseButton
{
    private FirebaseUser user;
    
    private string ServerID;
    private int AvatarIndex = 0;

    public TMP_InputField NameInput;
    public GameObject Char1;
    public GameObject Char2;
    public GameObject Char3;
    public GameObject Char4;

    private void Awake()
    {
        user = FirebaseConnection.instance.auth.CurrentUser;
        ServerID = PlayerPrefs.GetString("ServerID");

        SetRandomName();
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
        if (user == null)
        {
            Notification.instance.ShowNotifications("Bạn chưa đăng nhập");
            return;
        }

        string name = NameInput.text;

        // kiểm tra tên nhân vật có trống không
        if (name.Length == 0)
        {
            Notification.instance.ShowNotifications("Bạn chưa đặt tên cho nhân vật");
            return;
        }

        DatabaseReference reference = FirebaseConnection.instance.databaseReference.Child("accounts").Child(user.UserId).Child("servers").Child(ServerID);
        Dictionary<string, object> gamesData = new Dictionary<string, object>
        {
            { "id", ServerID+"SAH"+RandomStringGenerator.GenerateRandomString(7) },
            { "name", name },
            { "level", "0" },
            { "EXP", "0" },
            { "power", "0" },
            { "physical", "120" },
            { "vip", "0" },
            { "avatar", GetAvatarIndex() }
        };

        Dictionary<string, object> itemsData = new Dictionary<string, object>
        {
            { "diamond", "0" },
            { "meat", "0" },
            { "iron", "0" }
        };

        reference.Child("games").UpdateChildrenAsync(gamesData);
        reference.Child("items").UpdateChildrenAsync(itemsData);

        // chuyển sang màn hình game
        GetComponent<LoadScene>().LoadSelectScene();
    }

    protected override void OnClick()
    {
        SetGameDataAsync();
    }
}
