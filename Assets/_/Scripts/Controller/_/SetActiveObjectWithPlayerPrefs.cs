using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveObjectWithPlayerPrefs : MonoBehaviour
{
    public string playerPrefsKey;
    public string activeKey;

    public GameObject obj;
    private bool active = false;

    private void Awake()
    {
        if (obj == null) obj = gameObject;


        if (PlayerPrefs.GetString(playerPrefsKey) == activeKey)
        {
            obj.SetActive(true);
            active = true;
        }
        else
        {
            obj.SetActive(false);
            active = false;
        }
    }

    private void FixedUpdate()
    {
        if (PlayerPrefs.GetString(playerPrefsKey) == activeKey)
        {
            if (active == true) return;
            obj.SetActive(true);
            active = true;
        }
        else
        {
            if (active == false) return;
            obj.SetActive(false);
            active = false;
        }
    }

}
