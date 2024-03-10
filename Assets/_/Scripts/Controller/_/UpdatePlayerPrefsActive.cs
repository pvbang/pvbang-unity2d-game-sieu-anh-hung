using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePlayerPrefsActive : MonoBehaviour
{
    public string key = "";
    public bool isID = false;
    public string nameActive = "";
    public GameObject activeObject;

    private void FixedUpdate()
    {
        if (isID)
        {
            nameActive = gameObject.transform.name;
        }

        if (PlayerPrefs.GetString(key) == nameActive)
        {
            activeObject.SetActive(true);
        } else
        {
            activeObject.SetActive(false);
        }
    }
}
