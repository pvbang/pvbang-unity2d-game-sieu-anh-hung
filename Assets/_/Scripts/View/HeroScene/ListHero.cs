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

    void OnEnable()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        HeroManager.GetListHeroByAccount(heros =>
        {
            this.heros = heros;
        });

        yield return new WaitForSeconds(0.3f);
        if (this.heros == null) yield return new WaitForSeconds(0.5f);

        ShowListHeros(this.heros);
    }

    // hiển thị danh sách các servers đã chơi
    public void ShowListHeros(List<Hero> heros)
    {
        if (heros.Count == 0)
        {
            Transform itemTransform = Instantiate(this.itemBlank);
            contentAccountServer.DestroyContents();
            contentAccountServer.AddContent(itemTransform);
            return;
        }

        List<Transform> contentList = new List<Transform>();
        contentList.Add(Instantiate(this.itemBlank));
        foreach (Hero hero in heros)
        {
            Transform itemTransform = Instantiate(this.item);
            itemTransform.GetComponent<HeroFrame>().SetHeroFrame(hero);
            contentList.Add(itemTransform);
        }

        contentAccountServer.DestroyContents();
        contentAccountServer.AddContent(contentList);
    }

    // đảo ngược danh sách heros
    public void InvertList()
    {
        heros.Reverse();
        ShowListHeros(heros);
    }
}
