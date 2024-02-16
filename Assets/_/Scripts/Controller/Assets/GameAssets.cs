//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class GameAssets : MonoBehaviour
//{
//    private static GameAssets _instance;
//    public static GameAssets Instance => _instance;

//    public GameObject[] games;
//    public GameObject[] heros;
//    public GameObject[] items;
//    public GameObject[] icons;
//    public GameObject[] weapons;

//    private Dictionary<string, GameObject> assetMap = new Dictionary<string, GameObject>();

//    private void Awake()
//    {
//        if (_instance == null)
//        {
//            _instance = this;
//            DontDestroyOnLoad(gameObject); 
//            InitializeAssetMap();
//        }
//        else
//        {
//            Destroy(gameObject);
//        }
//    }

//    private void InitializeAssetMap()
//    {
//        // duyệt qua toàn bộ để thêm vào assetMap
//        foreach (var game in games)
//        {
//            string id = game.GetComponent<ID>().id;
//            if (id == "") id = game.GetComponentInChildren<ID>().id;
//            if (id == "") id = game.name;

//            assetMap.Add(id, game);
//        }
//        foreach (var hero in heros)
//        {
//            string id = hero.GetComponent<ID>().id;
//            if (id == "") id = hero.GetComponentInChildren<ID>().id;
//            if (id == "") id = hero.name;

//            assetMap.Add(id, hero);
//        }
//        foreach (var item in items)
//        {
//            string id = item.GetComponent<ID>().id;
//            if (id == "") id = item.GetComponentInChildren<ID>().id;
//            if (id == "") id = item.name;

//            assetMap.Add(id, item);
//        }
//        foreach (var icon in icons)
//        {
//            string id = icon.GetComponent<ID>().id;
//            if (id == "") id = icon.GetComponentInChildren<ID>().id;
//            if (id == "") id = icon.name;

//            assetMap.Add(id, icon);
//        }
//        foreach (var weapon in weapons)
//        {
//            string id = weapon.GetComponent<ID>().id;
//            if (id == "") id = weapon.GetComponentInChildren<ID>().id;
//            if (id == "") id = weapon.name;

//            assetMap.Add(id, weapon);
//        }
//    }

//    // lấy gameobject từ id
//    public GameObject GetGameObjectFromId(string id)
//    {
//        if (Instance.assetMap.ContainsKey(id))
//        {
//            Debug.Log("Asset: " + id);
//            return Instance.assetMap[id];
//        }
//        else
//        {
//            Debug.Log("Không tìm thấy Asset: " + id);
//            return null;
//        }
//    }
//}

using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _instance;
    public static GameAssets Instance => _instance;

    public GameObject[] games;
    public GameObject[] heros;
    public GameObject[] items;
    public GameObject[] icons;
    public GameObject[] weapons;

    // Sử dụng một dictionary để lưu trữ asset theo ID
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
        AddGameObjectsToMap(assetMap, games);
        AddGameObjectsToMap(assetMap, heros);
        AddGameObjectsToMap(assetMap, items);
        AddGameObjectsToMap(assetMap, icons);
        AddGameObjectsToMap(assetMap, weapons);
    }

    private void AddGameObjectsToMap(Dictionary<string, GameObject> map, GameObject[] objects)
    {
        foreach (var obj in objects)
        {
            ID idComponent = obj.GetComponent<ID>() ?? obj.GetComponentInChildren<ID>();
            string id = ((idComponent != null) && (idComponent.id != "")) ? idComponent.id : obj.name;

            if (!map.ContainsKey(id))
            {
                map.Add(id, obj);
            }
            else
            {
                Debug.LogWarning("ID trùng lặp: " + id + " cho GameObject: " + obj.name);
            }
        }
    }

    // Lấy gameobject từ ID
    public GameObject GetGameObjectFromId(string id)
    {
        if (assetMap.TryGetValue(id, out GameObject obj))
        {
            Debug.Log("Asset: " + id);
            return obj;
        }
        else
        {
            Debug.LogWarning("Asset không tồn tại: " + id);
            return null;
        }
    }


}

