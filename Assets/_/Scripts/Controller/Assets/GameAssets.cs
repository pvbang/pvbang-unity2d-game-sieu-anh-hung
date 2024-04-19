using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _instance;
    public static GameAssets Instance => _instance;

    public GameObject[] games;
    public GameObject[] heros;
    public GameObject[] specialHeros;
    public GameObject[] items;
    public GameObject[] icons;
    public GameObject[] weapons;
    public GameObject[] nguyenLieuChuyenSinh;

    // Sử dụng một dictionary để lưu trữ asset theo ID
    private Dictionary<string, GameObject> assetMap = new Dictionary<string, GameObject>();

    // Chỉ lưu gameobject heros
    private Dictionary<string, GameObject> assetMapHero = new Dictionary<string, GameObject>();

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
        AddGameObjectsToMap(assetMap, specialHeros);
        AddGameObjectsToMap(assetMap, items);
        AddGameObjectsToMap(assetMap, icons);
        AddGameObjectsToMap(assetMap, weapons);
        AddGameObjectsToMap(assetMap, nguyenLieuChuyenSinh);

        AddGameObjectsToMap(assetMapHero, heros);
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
            // Debug.Log("Assets: " + id);
            return obj;
        }
        else
        {
            Debug.LogWarning("Assets không tồn tại: " + id);
            return null;
        }
    }


    // Lấy random một gameobject trong heros 
    public GameObject GetRandomGameObjectHero()
    {
        // Kiểm tra nếu mảng không rỗng
        if (heros.Length > 0)
        {
            int randomIndex = Random.Range(0, heros.Length);
            GameObject randomHero = heros[randomIndex];
            
            return randomHero;
        }
        else
        {
            Debug.LogError("Không có heros");
            return null;
        }
    }

    // lấy danh sách tất cả heros
    public GameObject[] GetAllHeros()
    {
        return heros;
    }

    // lấy danh sách nguyên liệu chuyển sinh
    public GameObject[] GetNguyenLieuChuyenSinh()
    {
        return nguyenLieuChuyenSinh;
    }
}

