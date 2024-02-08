using Firebase.Database;
using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HeroBar : MonoBehaviour
{
    private FirebaseUser user;
    private string ServerID;

    public GameObject Avatar;
    public TextMeshProUGUI TxtName;
    public TextMeshProUGUI TxtLevel;
    public GameObject EXP;
    public GameObject Physial;
    public TextMeshProUGUI TxtMeat;
    public TextMeshProUGUI TxtDiamond;
    public TextMeshProUGUI TxtVip;

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

        DatabaseReference referenceItems = FirebaseDatabase.DefaultInstance.GetReference("accounts").Child(user.UserId).Child("servers").Child(ServerID).Child("items");
        referenceItems.ValueChanged += HandleValueChangedItems;
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

    // lấy thông tin nhân vật mỗi khi có thay đổi trong games
    void HandleValueChangedItems(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }

        // lấy dữ liệu
        DataSnapshot snapshot = args.Snapshot;
        SetInfoUserItems(snapshot);
    }


    // set thông tin nhân vật
    public void SetInfoUserGames(DataSnapshot data)
    {
        int avatar = int.Parse(data.Child("avatar").Value.ToString());
        string name = data.Child("name").Value.ToString();
        string level = data.Child("level").Value.ToString();
        string exp = data.Child("exp").Value.ToString();
        string maxEXP = data.Child("maxEXP").Value.ToString();
        string physical = data.Child("physical").Value.ToString();
        string maxPhysical = data.Child("maxPhysical").Value.ToString();
        string vip = data.Child("vip").Value.ToString();

        TxtName.text = name;
        TxtLevel.text = level;

        EXP.GetComponent<HealthBar>().UpdateBar(float.Parse(exp), float.Parse(maxEXP));
        Physial.GetComponent<HealthBar>().UpdateBar(float.Parse(physical), float.Parse(maxPhysical));

        TxtVip.text = "VIP " +vip;

        Avatar.GetComponent<BackgroundController>().ChangeBackground(avatar-1);

        EXP.GetComponent<EXPController>().CheckEXP(int.Parse(exp), int.Parse(maxEXP), int.Parse(level));
    }

    // set thông tin item
    public void SetInfoUserItems(DataSnapshot data)
    {
        string meat = data.Child("meat").Value.ToString();
        string diamond = data.Child("diamond").Value.ToString();

        TxtMeat.text = meat;
        TxtDiamond.text = diamond;
    }
}
