using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using TMPro;
using Unity.VisualScripting;
using Firebase.Database;
using Firebase.Extensions;
using static UnityEditor.PlayerSettings;

public class ListHero : MonoBehaviour
{
    public Content contents;
    public Transform itemBlank;
    public Transform itemAllHeros;
    public Transform itemMyHeros;
    public Transform itemMyHeroTeam;

    private List<Hero> heros;

    // danh sách hero hiện tại của game
    public bool allHeros = true;
    // tất cả hero đang có
    public bool myHeros = false;
    // tất cả hero trừ những hero đã ra trận
    public bool myHeroTeam = false;

    private void Awake()
    {
        if (itemBlank == null) itemBlank = GameAssets.Instance.GetGameObjectFromId("Item_Frame_Blank").transform;
        if (itemAllHeros == null) itemAllHeros = GameAssets.Instance.GetGameObjectFromId("Item_Hero_Frame_All").transform;
        if (itemMyHeros == null) itemMyHeros = GameAssets.Instance.GetGameObjectFromId("Item_Hero_Frame_My").transform;
        if (itemMyHeroTeam == null) itemMyHeroTeam = GameAssets.Instance.GetGameObjectFromId("Item_Hero_Frame_My_Team").transform;
        if (contents == null) contents = gameObject.GetComponentInChildren<Content>();

        this.heros = new List<Hero>();
    }

    private void Start()
    {
        if (allHeros)
        {
            LoadAllHeros();
        } else if (myHeros)
        {
            StartCoroutine(LoadMyHeros());
        } else if (myHeroTeam)
        {
            StartCoroutine(LoadTeamHeros());
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////
    // load danh sach tat ca hero
    void LoadAllHeros()
    {
        WaitingController.Instance.StartWaiting();
        GameObject[] listAllHeros = GameAssets.Instance.GetAllHeros();
        // duyệt hết listAllHeros để lấy ra danh sách hero
        foreach (GameObject hero in listAllHeros)
        {
            HeroUnit heroUnit = hero.GetComponent<HeroUnit>();
            if (heroUnit != null)
            {
                this.heros.Add(heroUnit.ToHero());
            }
        }

        SortByPower();
        ShowAllHeros(this.heros);
    }

    // hiển thị danh sách tất cả heros
    public void ShowAllHeros(List<Hero> heros)
    {
        if (heros == null)
        {
            contents.DestroyContents();

            WaitingController.Instance.EndWaiting();
            return;
        }

        List<Transform> contentList = new List<Transform>();
        foreach (Hero hero in heros)
        {
            Transform itemTransform = Instantiate(this.itemAllHeros);
            itemTransform.name = hero.h_id;
            itemTransform.GetComponent<HeroFrame>().SetHeroFrame(hero);
            contentList.Add(itemTransform);
        }

        contents.DestroyContents();
        contents.AddContent(contentList);

        WaitingController.Instance.EndWaiting();
    }

    ////////////////////////////////////////////////////////////////////////////////////////////
    // load danh sach tat ca hero trong tài khoản
    IEnumerator LoadMyHeros()
    {
        WaitingController.Instance.StartWaiting();
        bool isLoaded = false;
        HeroManager.GetListHeroByAccount(heros =>
        {
            this.heros = heros;
            isLoaded = true;
        });

        while (isLoaded == false)
        {
            yield return new WaitForSeconds(0.1f);
        }

        SortByPower();
        ShowMyHeros(this.heros);
    }

    // hiển thị danh sách tất cả heros trong tài khoản
    public void ShowMyHeros(List<Hero> heros)
    {
        if (heros == null)
        {
            contents.DestroyContents();

            WaitingController.Instance.EndWaiting();
            return;
        }

        List<Transform> contentList = new List<Transform>();
        foreach (Hero hero in heros)
        {
            Transform itemTransform = Instantiate(this.itemMyHeros);
            itemTransform.name = hero.id;
            itemTransform.GetComponent<HeroFrame>().SetHeroFrame(hero);
            contentList.Add(itemTransform);
        }

        contents.DestroyContents();
        contents.AddContent(contentList);

        WaitingController.Instance.EndWaiting();
    }

    ////////////////////////////////////////////////////////////////////////////////////////////
    // load danh sach tat ca hero trong tài khoản trừ những hero đã ra trận
    IEnumerator LoadTeamHeros()
    {
        WaitingController.Instance.StartWaiting();
        bool isLoaded = false;
        HeroManager.GetListHeroByAccount(heros =>
        {
            this.heros = heros;
            isLoaded = true;
        });

        while (isLoaded == false)
        {
            yield return new WaitForSeconds(0.1f);
        }

        SortByPower();
        isLoaded = false;

        FirebaseDatabase.DefaultInstance.GetReference("accounts").Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId).Child("servers").Child(PlayerPrefs.GetString("ServerID")).Child("teams").Child(PlayerPrefs.GetString("Teams_ID")).GetValueAsync().ContinueWithOnMainThread(task => {
            if (task.IsFaulted)
            {
                Debug.Log("Lỗi lấy thông tin team");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot data = task.Result;

                string position_1 = (data.Child("position_1").Exists) ? data.Child("position_1").Value.ToString() : null;
                string position_2 = (data.Child("position_2").Exists) ? data.Child("position_2").Value.ToString() : null;
                string position_3 = (data.Child("position_3").Exists) ? data.Child("position_3").Value.ToString() : null;
                string position_4 = (data.Child("position_4").Exists) ? data.Child("position_4").Value.ToString() : null;
                string position_5 = (data.Child("position_5").Exists) ? data.Child("position_5").Value.ToString() : null;
                string position_6 = (data.Child("position_6").Exists) ? data.Child("position_6").Value.ToString() : null;
                string position_7 = (data.Child("position_7").Exists) ? data.Child("position_7").Value.ToString() : null;
                string position_8 = (data.Child("position_8").Exists) ? data.Child("position_8").Value.ToString() : null;
                string position_9 = (data.Child("position_9").Exists) ? data.Child("position_9").Value.ToString() : null;

                // bỏ bớt những hero đã ra trận khỏi this.heros
                if (position_1 != null) this.heros.RemoveAll(hero => hero.id == position_1);
                if (position_2 != null) this.heros.RemoveAll(hero => hero.id == position_2);
                if (position_3 != null) this.heros.RemoveAll(hero => hero.id == position_3);
                if (position_4 != null) this.heros.RemoveAll(hero => hero.id == position_4);
                if (position_5 != null) this.heros.RemoveAll(hero => hero.id == position_5);
                if (position_6 != null) this.heros.RemoveAll(hero => hero.id == position_6);
                if (position_7 != null) this.heros.RemoveAll(hero => hero.id == position_7);
                if (position_8 != null) this.heros.RemoveAll(hero => hero.id == position_8);
                if (position_9 != null) this.heros.RemoveAll(hero => hero.id == position_9);
            }

            isLoaded = true;
        });

        while (isLoaded == false)
        {
            yield return new WaitForSeconds(0.1f);
        }

        ShowTeamHeros(this.heros);
    }

    // hiển thị danh sách tất cả heros trong tài khoản trừ những hero đã ra trận
    public void ShowTeamHeros(List<Hero> heros)
    {
        Transform itemBlank = Instantiate(this.itemBlank);
        itemBlank.name = "HERO_BLANK";

        if (heros == null)
        {
            contents.DestroyContents();
            contents.AddContent(itemBlank);

            WaitingController.Instance.EndWaiting();
            return;
        }

        List<Transform> contentList = new List<Transform>();
        contentList.Add(itemBlank);
        foreach (Hero hero in heros)
        {
            Transform itemTransform = Instantiate(this.itemMyHeroTeam);
            itemTransform.name = hero.id;
            itemTransform.GetComponent<HeroFrame>().SetHeroFrame(hero);
            contentList.Add(itemTransform);
        }

        contents.DestroyContents();
        contents.AddContent(contentList);

        WaitingController.Instance.EndWaiting();
    }

    ////////////////////////////////////////////////////////////////////////////////////////////

    // đảo ngược danh sách heros
    public void InvertList()
    {
        heros.Reverse();
        if (allHeros)
        {
            ShowAllHeros(heros);
        }
        else if (myHeros)
        {
            ShowMyHeros(heros);
        }
        else if (myHeroTeam)
        {
            ShowTeamHeros(heros);
        }
    }

    // tính lực chiến của hero
    public ulong CalculatePower(Hero hero)
    {
        ulong power = 0;
        power += (ulong)hero.h_level;
        power += (ulong)hero.h_exp;
        power += hero.h_damagePysical;
        power += hero.h_damageMagic;
        power += hero.h_tank;
        power += hero.h_speed;
        power += (ulong)hero.evolution * 10;
        power += (ulong)hero.superEvolution * 10;
        power += (ulong)hero.reincarnation * 10;
        power += (ulong)hero.transformation * 10;

        return power;
    }

    // sắp xếp list tất cả heros theo lực chiến giảm dần
    public void SortByPower()
    {
        if (this.heros == null) return; 
        this.heros.Sort((x, y) => CalculatePower(y).CompareTo(CalculatePower(x)));
    }
}
