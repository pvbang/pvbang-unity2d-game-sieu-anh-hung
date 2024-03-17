using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class Reincarnation : MonoBehaviour
{
    public Content contents;
    public Transform Item_Hero_Banner;

    public ContentReincarnation contentReincarnation;
    private List<Hero> heros;

    private string Choice_Item_Hero_Banner = "";

    private void Awake()
    {
        if (contents == null) contents = gameObject.GetComponentInChildren<Content>();
        if (Item_Hero_Banner == null) Item_Hero_Banner = GameAssets.Instance.GetGameObjectFromId("Item_Hero_Banner").transform;

        this.heros = new List<Hero>();
    }

    private void Start()
    {
        StartCoroutine(LoadMyHeros());
    }

    private void FixedUpdate()
    {
        if (Choice_Item_Hero_Banner != PlayerPrefs.GetString("Choice_Item_Hero_Banner", ""))
        {
            Choice_Item_Hero_Banner = PlayerPrefs.GetString("Choice_Item_Hero_Banner", "");

            if (Choice_Item_Hero_Banner == "") return;
            Hero __hero = GetHeroFromId(Choice_Item_Hero_Banner.Replace("(Clone)", ""));
            contentReincarnation.SetUI(__hero);
        }
    }

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
        StartCoroutine(ShowMyHeros(this.heros));
    }

    // hiển thị danh sách tất cả heros trong tài khoản
    IEnumerator ShowMyHeros(List<Hero> heros)
    {
        if (heros == null)
        {
            contents.DestroyContents();

            WaitingController.Instance.EndWaiting();
            yield break;
        }

        // lấy hero đầu tiên
        contentReincarnation.SetUI(heros[0]);
        PlayerPrefs.SetString("Choice_Item_Hero_Banner", heros[0].id+"(Clone)");

        List<Transform> contentList = new List<Transform>();

        bool isLoaded = false;

        Position position = new Position();

        // lấy thông tin hero từ position
        _Teams.GetTeam(PlayerPrefs.GetString("Teams_ID", "Main_1"), _posision =>
        {
            position = _posision;
            isLoaded = true;
        });

        while (isLoaded == false)
        {
            yield return new WaitForSeconds(0.1f);
        }

        foreach (Hero hero in heros)
        {
            Transform itemTransform = Instantiate(this.Item_Hero_Banner);
            itemTransform.name = hero.id;

            string notification = "";
            
            if (position != null) {
                // kiểm tra hero.id có trong position không
                if (position.position_1 == hero.id || position.position_2 == hero.id || position.position_3 == hero.id || position.position_4 == hero.id || position.position_5 == hero.id || position.position_6 == hero.id || position.position_7 == hero.id || position.position_8 == hero.id || position.position_9 == hero.id)
                {
                    notification = "Ra trận";
                }
            }

            Transform heroTransform = GameAssets.Instance.GetGameObjectFromId(hero.h_id).transform;
            ItemAssets itemAssets = heroTransform.GetComponent<ItemAssets>();

            itemTransform.GetComponent<ItemHeroBanner>().SetUI(hero.h_level, hero.h_name, notification, itemAssets.GetIcon(), itemAssets.GetFrame(), hero.superEvolution, hero.reincarnation);
            contentList.Add(itemTransform);
        }

        contents.DestroyContents();
        contents.AddContent(contentList);

        WaitingController.Instance.EndWaiting();
    }


    /////////////////////////////////////////////////////////////////////////////////////////
    
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

    //////////////////////////////////////////////////////////////////////
    // sắp xếp list tất cả heros theo lực chiến giảm dần
    public void SortByPower()
    {
        if (this.heros == null) return;
        this.heros.Sort((x, y) => CalculatePower(y).CompareTo(CalculatePower(x)));
    }

    // button sắp xếp theo lực chiến
    public void OnClickSortByPower()
    {
        WaitingController.Instance.StartWaiting();
        SortByPower();
        StartCoroutine(ShowMyHeros(this.heros));
    }

    //////////////////////////////////////////////////////////////////////
    // sắp xếp theo cấp độ
    public void SortByLevel()
    {
        if (this.heros == null) return;
        this.heros.Sort((x, y) => y.h_level.CompareTo(x.h_level));
    }

    // button sắp xếp theo cấp độ
    public void OnClickSortByLevel()
    {
        WaitingController.Instance.StartWaiting();
        SortByLevel();
        StartCoroutine(ShowMyHeros(this.heros));
    }

    //////////////////////////////////////////////////////////////////////
    // sắp xếp theo chuyển sinh
    public void SortByReincarnation()
    {
        if (this.heros == null) return;
        this.heros.Sort((x, y) => y.reincarnation.CompareTo(x.reincarnation));
    }

    // button sắp xếp theo chuyển sinh
    public void OnClickSortByReincarnation()
    {
        WaitingController.Instance.StartWaiting();
        SortByReincarnation();
        StartCoroutine(ShowMyHeros(this.heros));
    }

    //////////////////////////////////////////////////////////////////////
    // sắp xếp theo siêu tiến hóa
    public void SortBySuperEvolution()
    {
        if (this.heros == null) return;
        this.heros.Sort((x, y) => y.superEvolution.CompareTo(x.superEvolution));
    }

    // button sắp xếp theo siêu tiến hóa
    public void OnClickSortBySuperEvolution()
    {
        WaitingController.Instance.StartWaiting();
        SortBySuperEvolution();
        StartCoroutine(ShowMyHeros(this.heros));
    }

    // lấy hero từ id
    public Hero GetHeroFromId(string id)
    {
        foreach (Hero hero in this.heros)
        {
            if (hero.id == id)
            {
                return hero;
            }
        }

        return null;
    }
}
