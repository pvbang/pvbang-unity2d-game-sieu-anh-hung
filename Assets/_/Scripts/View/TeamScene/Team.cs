using Firebase.Auth;
using Firebase.Database;
using UnityEngine;
using TMPro;

public class Team : MonoBehaviour
{
    private FirebaseUser user;
    private string ServerID;

    public TextMeshProUGUI TxtPower;
    public GameObject pos1;
    public GameObject pos2;
    public GameObject pos3;
    public GameObject pos4;
    public GameObject pos5;
    public GameObject pos6;
    public GameObject pos7;
    public GameObject pos8;
    public GameObject pos9;

    public GameObject Properties;

    private void Awake()
    {
        user = FirebaseAuth.DefaultInstance.CurrentUser;

        ServerID = PlayerPrefs.GetString("ServerID");
    }

    private void Start()
    {
        if (user == null) return;
        if (ServerID == "") return;

        DatabaseReference referenceGames = FirebaseDatabase.DefaultInstance.GetReference("accounts").Child(user.UserId).Child("servers").Child(ServerID).Child("games");
        referenceGames.ValueChanged += HandleValueChangedGames;

        DatabaseReference referenceItems = FirebaseDatabase.DefaultInstance.GetReference("accounts").Child(user.UserId).Child("servers").Child(ServerID).Child("teams");
        referenceItems.ValueChanged += HandleValueChangedTeams;
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
    void HandleValueChangedTeams(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }

        // lấy dữ liệu
        DataSnapshot snapshot = args.Snapshot;
        SetInfoUserTeams(snapshot);
    }


    // set thông tin nhân vật
    public void SetInfoUserGames(DataSnapshot data)
    {
        //int avatar = int.Parse(data.Child("avatar").Value.ToString());
        //string name = data.Child("name").Value.ToString();
        //string level = data.Child("level").Value.ToString();
        //string exp = data.Child("exp").Value.ToString();
        //string maxEXP = data.Child("maxEXP").Value.ToString();
        //string physical = data.Child("physical").Value.ToString();
        //string maxPhysical = data.Child("maxPhysical").Value.ToString();
        //string vip = data.Child("vip").Value.ToString();
        string power = data.Child("power").Value.ToString();

        //if (TxtName != null) TxtName.text = name;
        //if (TxtLevel != null) TxtLevel.text = level;

        //if (EXP != null) EXP.GetComponent<HealthBar>().UpdateBar(float.Parse(exp), float.Parse(maxEXP));
        //if (Physial != null) Physial.GetComponent<HealthBar>().UpdateBar(float.Parse(physical), float.Parse(maxPhysical));

        //if (TxtVip != null) TxtVip.text = "VIP " + vip;
        if (TxtPower != null) TxtPower.text = power;

        //if (Avatar != null) Avatar.GetComponent<BackgroundController>().ChangeBackground(avatar - 1);

        //if (EXP != null) EXP.GetComponent<EXPController>().CheckEXP(int.Parse(exp), int.Parse(maxEXP), int.Parse(level));
        //if (Physial != null) Physial.GetComponent<PhysicalController>().CheckPhysical(int.Parse(physical), int.Parse(maxPhysical));
    }

    // set thông tin team
    public void SetInfoUserTeams(DataSnapshot data)
    {
        //string meat = data.Child("meat").Value.ToString();
        //string diamond = data.Child("diamond").Value.ToString();

        //TxtMeat.text = meat;
        //TxtDiamond.text = diamond;
    }
}
