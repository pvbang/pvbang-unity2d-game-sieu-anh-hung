using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Firebase.Auth;
using Firebase.Extensions;

public class HeroPos : MonoBehaviour
{
    public GameObject pos1;
    public GameObject pos2;
    public GameObject pos3;
    public GameObject pos4;
    public GameObject pos5;

    private void Awake()
    {
        Setup();
    }

    public void Setup()
    {
        if (pos1 == null) pos1 = gameObject.transform.Find("HeroPos1").gameObject;
        if (pos2 == null) pos2 = gameObject.transform.Find("HeroPos2").gameObject;
        if (pos3 == null) pos3 = gameObject.transform.Find("HeroPos3").gameObject;
        if (pos4 == null) pos4 = gameObject.transform.Find("HeroPos4").gameObject;
        if (pos5 == null) pos5 = gameObject.transform.Find("HeroPos5").gameObject;

        string teamID = PlayerPrefs.GetString("Teams_ID");
        // nếu teamID không tồn tại thì không cần lấy thông tin
        if (teamID == "") return;

        // nếu teamID không chứa "Main_" thì set teamID = "Main_1"
        if (!teamID.Contains("Main_")) teamID = "Main_1";
        FirebaseDatabase.DefaultInstance.GetReference("accounts").Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId).Child("servers").Child(PlayerPrefs.GetString("ServerID")).Child("teams").Child(teamID).GetValueAsync().ContinueWithOnMainThread(task => {
            if (task.IsFaulted)
            {
                Debug.Log("Lỗi lấy thông tin team");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                SetInfoUserTeams(snapshot);
            }
        });
    }

    // set thông tin team
    public void SetInfoUserTeams(DataSnapshot data)
    {
        string position_1 = (data.Child("position_1").Exists) ? data.Child("position_1").Value.ToString() : null;
        string position_2 = (data.Child("position_2").Exists) ? data.Child("position_2").Value.ToString() : null;
        string position_3 = (data.Child("position_3").Exists) ? data.Child("position_3").Value.ToString() : null;
        string position_4 = (data.Child("position_4").Exists) ? data.Child("position_4").Value.ToString() : null;
        string position_5 = (data.Child("position_5").Exists) ? data.Child("position_5").Value.ToString() : null;
        string position_6 = (data.Child("position_6").Exists) ? data.Child("position_6").Value.ToString() : null;

        // luôn luôn chỉ có 5 hero

        if (position_1 != null && pos1 != null) StartCoroutine(GetHeroInfo(position_1, pos1));
        if (position_2 != null && pos2 != null) StartCoroutine(GetHeroInfo(position_2, pos2));
        if (position_3 != null && pos3 != null) StartCoroutine(GetHeroInfo(position_3, pos3));
        if (position_4 != null && pos4 != null) StartCoroutine(GetHeroInfo(position_4, pos4));
        if (position_5 != null && pos5 != null) StartCoroutine(GetHeroInfo(position_5, pos5));

        GameObject pos6 = null;
        if (position_1 == null) pos6 = pos1;
        if (position_2 == null) pos6 = pos2;
        if (position_3 == null) pos6 = pos3;
        if (position_4 == null) pos6 = pos4;
        if (position_5 == null) pos6 = pos5;

        if (position_6 != null && pos6 != null) StartCoroutine(GetHeroInfo(position_6, pos6));
    }

    IEnumerator GetHeroInfo(string id, GameObject pos)
    {
        WaitingController.Instance.StartWaiting();

        if (id == "HERO_BLANK")
        {
            SetHeroUI(null, pos);
            WaitingController.Instance.EndWaiting();
            yield break;
        }

        Hero hero = new Hero();
        bool isLoaded = false;

        HeroManager.GetHeroInfoById(id, _hero =>
        {
            hero = _hero;
            isLoaded = true;
        });

        while (isLoaded == false)
        {
            yield return new WaitForSeconds(0.1f);
        }

        SetHeroUI(hero, pos);
    }

    void SetHeroUI(Hero hero, GameObject pos)
    {
        if (hero == null)
        {
            WaitingController.Instance.EndWaiting();
            return;
        }

        Transform heroTransform = pos.transform.Find("HeroPosButton").transform;
        GameObject heroObject = GameAssets.Instance.GetGameObjectFromId(hero.h_id);

        if (heroTransform != null && heroObject != null)
        {
            // hiển thị hero
            GameObject heroInstance = Instantiate(heroObject, heroTransform.position, heroTransform.rotation);
            heroInstance.transform.localScale = heroTransform.localScale;
            heroInstance.transform.Find("CanvasStatusBar").gameObject.SetActive(false);
            heroInstance.transform.Find("Position").gameObject.SetActive(false);
            heroInstance.transform.SetParent(this.transform);
        }
        WaitingController.Instance.EndWaiting();
    }
}
