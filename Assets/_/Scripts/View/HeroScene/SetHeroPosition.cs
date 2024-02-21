using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetHeroPosition : BaseButton
{
    // thêm hero vào vị trí trong team
    IEnumerator AddHeroPosition(string TeamID, string Position, string HeroID) {
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
        StartCoroutine(AddHeroPosition(TeamID, Position, HeroID));
    }
}
