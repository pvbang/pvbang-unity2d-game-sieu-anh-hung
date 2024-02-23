using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Team : MonoBehaviour
{
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

    public Sprite mainBlankBackground;
    public Sprite mainHeroBackground;

    public Sprite reserveBlankBackground;
    public Sprite reserveHeroBackground;

    public GameObject Properties;

    private void Awake()
    {
        ServerID = PlayerPrefs.GetString("ServerID");

        if (pos1 == null) pos1 = gameObject.transform.Find("Pos1").Find("Pos1").gameObject;
        if (pos2 == null) pos2 = gameObject.transform.Find("Pos2").Find("Pos2").gameObject;
        if (pos3 == null) pos3 = gameObject.transform.Find("Pos3").Find("Pos3").gameObject;
        if (pos4 == null) pos4 = gameObject.transform.Find("Pos4").Find("Pos4").gameObject;
        if (pos5 == null) pos5 = gameObject.transform.Find("Pos5").Find("Pos5").gameObject;
        if (pos6 == null) pos6 = gameObject.transform.Find("Pos6").Find("Pos6").gameObject;
        if (pos7 == null) pos7 = gameObject.transform.Find("Pos7").Find("Pos7").gameObject;
        if (pos8 == null) pos8 = gameObject.transform.Find("Pos8").Find("Pos8").gameObject;
        if (pos9 == null) pos9 = gameObject.transform.Find("Pos9").Find("Pos9").gameObject;
    }

    private void Start()
    {
        if (FirebaseAuth.DefaultInstance.CurrentUser == null) return;
        if (ServerID == "") return;

        DatabaseReference referenceGames = FirebaseDatabase.DefaultInstance.GetReference("accounts").Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId).Child("servers").Child(ServerID).Child("games");
        referenceGames.ValueChanged += HandleValueChangedGames;

        DatabaseReference referenceItems = FirebaseDatabase.DefaultInstance.GetReference("accounts").Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId).Child("servers").Child(ServerID).Child("teams").Child(PlayerPrefs.GetString("Teams_ID"));
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
        string power = data.Child("power").Value.ToString();
        if (TxtPower != null) TxtPower.text = power;
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
        string position_7 = (data.Child("position_7").Exists) ? data.Child("position_7").Value.ToString() : null;
        string position_8 = (data.Child("position_8").Exists) ? data.Child("position_8").Value.ToString() : null;
        string position_9 = (data.Child("position_9").Exists) ? data.Child("position_9").Value.ToString() : null;

        if (position_1 != null && pos1 != null) StartCoroutine(GetHeroInfo(position_1, pos1, 1));
        if (position_2 != null && pos2 != null) StartCoroutine(GetHeroInfo(position_2, pos2, 2));
        if (position_3 != null && pos3 != null) StartCoroutine(GetHeroInfo(position_3, pos3, 3));
        if (position_4 != null && pos4 != null) StartCoroutine(GetHeroInfo(position_4, pos4, 4));
        if (position_5 != null && pos5 != null) StartCoroutine(GetHeroInfo(position_5, pos5, 5));
        if (position_6 != null && pos6 != null) StartCoroutine(GetHeroInfo(position_6, pos6, 6));
        if (position_7 != null && pos7 != null) StartCoroutine(GetHeroInfo(position_7, pos7, 7));
        if (position_8 != null && pos8 != null) StartCoroutine(GetHeroInfo(position_8, pos8, 8));
        if (position_9 != null && pos9 != null) StartCoroutine(GetHeroInfo(position_9, pos9, 9));
    }

    IEnumerator GetHeroInfo(string id, GameObject pos, int posNum)
    {
        WaitingController.Instance.StartWaiting();

        if (id == "HERO_BLANK") {
            Sprite blankSprite = posNum <= 6 ? mainBlankBackground : reserveBlankBackground;
            SetHeroUI(null, pos, blankSprite);
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

        Sprite herroSprite = posNum <= 6 ? mainHeroBackground : reserveHeroBackground;
        SetHeroUI(hero, pos, herroSprite);
    }

    void SetHeroUI(Hero hero, GameObject pos, Sprite background)
    {
        pos.GetComponent<Image>().sprite = background;

        if (hero == null)
        {
            WaitingController.Instance.EndWaiting();
            return;
        }

        Transform heroTransform = pos.transform.Find("Hero").transform;
        GameObject heroObject = GameAssets.Instance.GetGameObjectFromId(hero.h_id);

        if (heroTransform != null && heroObject != null)
        {
            GameObject heroInstance = Instantiate(heroObject, heroTransform.position, heroTransform.rotation);
            heroInstance.transform.localScale = heroTransform.localScale;
            heroInstance.transform.Find("CanvasStatusBar").gameObject.SetActive(false);
            heroInstance.transform.SetParent(this.transform);
        }
        WaitingController.Instance.EndWaiting();
    }

}
