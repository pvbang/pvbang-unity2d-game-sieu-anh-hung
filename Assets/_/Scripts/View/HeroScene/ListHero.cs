using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using TMPro;
using Unity.VisualScripting;

public class ListHero : MonoBehaviour
{
    private FirebaseUser user;

    public Content contentAccountServer;
    public Transform itemBlank;
    public Transform item;

    private List<Hero> heros;

    private void Awake()
    {
        user = FirebaseAuth.DefaultInstance.CurrentUser;
        if (itemBlank == null) itemBlank = GameAssets.Instance.GetGameObjectFromId("Item_Frame_Blank").transform;
        if (item == null) item = GameAssets.Instance.GetGameObjectFromId("Item_Hero_Frame").transform;
        if (contentAccountServer == null) contentAccountServer = gameObject.GetComponentInChildren<Content>();

        this.heros = new List<Hero>();
    }

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
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

        ShowListHeros(this.heros);
    }

    // hiển thị danh sách các servers đã chơi
    public void ShowListHeros(List<Hero> heros)
    {
        Transform itemBlank = Instantiate(this.itemBlank);
        itemBlank.name = "HERO_BLANK";

        if (heros == null)
        {
            contentAccountServer.DestroyContents();
            contentAccountServer.AddContent(itemBlank);

            WaitingController.Instance.EndWaiting();
            return;
        }

        List<Transform> contentList = new List<Transform>();
        contentList.Add(itemBlank);
        foreach (Hero hero in heros)
        {
            Transform itemTransform = Instantiate(this.item);
            itemTransform.name = hero.id;
            itemTransform.GetComponent<HeroFrame>().SetHeroFrame(hero);
            contentList.Add(itemTransform);
        }

        contentAccountServer.DestroyContents();
        contentAccountServer.AddContent(contentList);

        WaitingController.Instance.EndWaiting();
    }

    // đảo ngược danh sách heros
    public void InvertList()
    {
        heros.Reverse();
        ShowListHeros(heros);
    }
}
