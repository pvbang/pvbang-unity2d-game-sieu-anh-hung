using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class Waiting : MonoBehaviour
{
    public Transform position;

    private GameObject heroInstance = null;

    private void OnEnable()
    {
        // Lấy hero random
        GameObject heroObject = GameAssets.Instance.GetRandomGameObjectHero();

        // Lấy hero chỉ định
        // GameObject heroObject = GameAssets.Instance.GetGameObjectFromId("Hero_Overlord");

        if (heroObject == null) return;
        heroInstance = Instantiate(heroObject, position.position, position.rotation);
        heroInstance.transform.Find("CanvasStatusBar").gameObject.SetActive(false);
        heroInstance.GetComponent<SortingGroup>().sortingLayerName = "AOA";
        heroInstance.GetComponent<SortingGroup>().sortingOrder = 9999;
    }

    private void OnDisable()
    {
        Destroy(heroInstance);
    }
}
