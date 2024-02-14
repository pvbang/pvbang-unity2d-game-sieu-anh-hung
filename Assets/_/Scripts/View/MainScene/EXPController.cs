using Firebase.Auth;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPController : MonoBehaviour
{
    private FirebaseUser user;
    private string ServerID;

    private DatabaseReference referenceGames;

    private void Awake()
    {
        user = FirebaseAuth.DefaultInstance.CurrentUser;
        ServerID = PlayerPrefs.GetString("ServerID");
    }

    void Start()
    {
        if (user == null) return;
        if (ServerID == "") return;

        referenceGames = FirebaseDatabase.DefaultInstance.GetReference("accounts").Child(user.UserId).Child("servers").Child(ServerID).Child("games");
        //referenceGames.ValueChanged += HandleValueChangedGames;
    }

    // cập nhật thông tin EXP
    public void UpdateEXP(int exp)
    {
        referenceGames.Child("exp").SetValueAsync(exp);
    }

    // kiểm tra nếu EXP >= MaxEXP thì tăng level+1 trên database
    public void CheckEXP(int exp, int maxEXP, int level)
    {
        if (exp >= maxEXP)
        {
            if (level >= 999)
            {
                // nếu level >= 999 thì không tăng level nữa và không cho tăng EXP
                referenceGames.Child("exp").SetValueAsync(maxEXP);
                return;
            }

            // reset EXP, tăng maxEXP
            referenceGames.Child("exp").SetValueAsync(0);
            referenceGames.Child("maxEXP").SetValueAsync(maxEXP+(level*10));

            // tăng level
            referenceGames.Child("level").SetValueAsync(level+1);

            // tăng maxPhysial
            level = 120 + (level * 10);
            referenceGames.Child("maxPhysical").SetValueAsync(level);
        }
    }
}
