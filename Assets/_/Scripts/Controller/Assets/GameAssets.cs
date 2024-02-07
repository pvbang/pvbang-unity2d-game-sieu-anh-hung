using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _instance;

    public static GameAssets Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (Instantiate(Resources.Load("GameAssets")) as GameObject).GetComponent<GameAssets>();
            }
            return _instance;
        }
    }

    public Transform blankFrame;

    // overlord
    public Transform overlord;
    public Transform overlordFrame1;

    // tatsumaki
    public Transform tatsumaki;
    public Transform tatsumakiFrame1;

    public static Transform GetHeroFromId(string id)
    {
        switch (id)
        {
            case "blank":
                return Instance.blankFrame;
            case "overlord":
                return Instance.overlord;
            case "tatsumaki":
                return Instance.tatsumaki;
            default:
                return null; 
        }
    }
}
