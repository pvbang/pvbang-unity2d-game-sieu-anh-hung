using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _instance;
    public static GameAssets Instance => _instance;

    public GameObject[] games;
    public GameObject[] heros;
    public GameObject[] items;
    public GameObject[] weapons;

    private Dictionary<string, GameObject> assetMap = new Dictionary<string, GameObject>();

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); 
            InitializeAssetMap();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeAssetMap()
    {
        // duyệt qua toàn bộ để thêm vào assetMap
        foreach (var game in games)
        {
            string id = game.GetComponent<ID>().id;
            if (id == "") id = game.GetComponentInChildren<ID>().id;

            assetMap.Add(id, game);
        }
        foreach (var hero in heros)
        {
            string id = hero.GetComponent<ID>().id;
            if (id == "") id = hero.GetComponentInChildren<ID>().id;

            assetMap.Add(id, hero);
        }
        foreach (var item in items)
        {
            string id = item.GetComponent<ID>().id;
            if (id == "") id = item.GetComponentInChildren<ID>().id;

            assetMap.Add(item.GetComponent<ID>().id, item);
        }
        foreach (var weapon in weapons)
        {
            string id = weapon.GetComponent<ID>().id;
            if (id == "") id = weapon.GetComponentInChildren<ID>().id;

            assetMap.Add(id, weapon);
        }
    }

    // lấy gameobject từ id
    public GameObject GetGameObjectFromId(string id)
    {
        if (Instance.assetMap.ContainsKey(id))
        {
            return Instance.assetMap[id];
        }
        else
        {
            Debug.Log("Không tìm thấy Asset: " + id);
            return null;
        }
    }
}
