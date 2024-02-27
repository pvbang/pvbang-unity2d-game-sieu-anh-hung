using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetHeroPosition : BaseButton
{
    protected override void OnClick()
    {
        string TeamID = PlayerPrefs.GetString("Teams_ID");
        string Position = PlayerPrefs.GetString("Position");
        string HeroID = transform.name.Replace("(Clone)", "");

        if (TeamID == "" || Position == "" || HeroID == "")
        {
            Debug.Log("Lỗi set vị trí hero");
            return;
        }
        StartCoroutine(_Teams.AddHeroToPosition(TeamID, Position, HeroID));
    }
}
