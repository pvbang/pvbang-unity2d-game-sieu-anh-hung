using Firebase.Auth;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientReincarnation : MonoBehaviour
{
    public Content contents;
    public Transform Item;

    private GameObject[] nguyenLieuChuyenSinh;

    private void Start()
    {
        ShowAllHeros();
    }

    // hiển thị danh sách tất cả heros
    public void ShowAllHeros()
    {
        nguyenLieuChuyenSinh = GameAssets.Instance.GetNguyenLieuChuyenSinh();

        if (nguyenLieuChuyenSinh == null)
        {
            contents.DestroyContents();

            WaitingController.Instance.EndWaiting();
            return;
        }

        List<Transform> contentList = new List<Transform>();
        foreach (GameObject nguyenlieu in nguyenLieuChuyenSinh)
        {
            ItemAssets itemAssets = nguyenlieu.GetComponent<ItemAssets>();
            Transform itemTransform = Instantiate(this.Item);
            itemTransform.GetComponent<ItemCS>().SetItem(itemAssets);

            contentList.Add(itemTransform);
        }

        contents.DestroyContents();
        contents.AddContent(contentList);

        WaitingController.Instance.EndWaiting();
    }
}
