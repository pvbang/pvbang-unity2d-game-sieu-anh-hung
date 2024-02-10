using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Firebase.Auth;
using System.Xml.Linq;

public class _games_all : MonoBehaviour
{
    private FirebaseUser user;
    private string ServerID;

    private void Awake()
    {
        user = FirebaseConnection.instance.auth.CurrentUser;

        ServerID = PlayerPrefs.GetString("ServerID");
    }

    private void Start()
    {
        if (user == null) return;
        if (ServerID == "") return;

        DatabaseReference referenceGames = FirebaseDatabase.DefaultInstance.GetReference("accounts").Child(user.UserId).Child("servers").Child(ServerID).Child("games");
        referenceGames.ValueChanged += HandleValueChangedGames;
    }

    // lấy thông tin nhân vật mỗi khi có thay đổi trong games
    void HandleValueChangedGames(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }

        // lấy dữ liệu
        DataSnapshot snapshot = args.Snapshot;
        SetInfoUserGames(snapshot);
    }

    // set thông tin nhân vật
    public void SetInfoUserGames(DataSnapshot data)
    {
        string id = data.Child("avatar").Value.ToString();
        string name = data.Child("name").Value.ToString();
        int level = int.Parse(data.Child("level").Value.ToString());
        int exp = int.Parse(data.Child("exp").Value.ToString());
        int maxEXP = int.Parse(data.Child("maxEXP").Value.ToString());
        int power = int.Parse(data.Child("power").Value.ToString());
        int physical = int.Parse(data.Child("physical").Value.ToString());
        int maxPhysical = int.Parse(data.Child("maxPhysical").Value.ToString());
        int vip = int.Parse(data.Child("vip").Value.ToString());
        int avatar = int.Parse(data.Child("avatar").Value.ToString());
    }
}
