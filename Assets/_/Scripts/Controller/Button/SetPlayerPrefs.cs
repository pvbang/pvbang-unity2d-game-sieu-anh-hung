using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerPrefs : BaseButton
{
    public string key = "";
    public bool isID = false;

    [Header("Int")]    
    public bool isInt = false;
    public int intValue;

    [Header("Float")]
    public bool isFloat = false;
    public float floatValue;

    [Header("String")]
    public bool isString = false;
    public string stringValue;

    protected override void OnClick()
    {
        if (isInt)
        {
            PlayerPrefs.SetInt(key, intValue);
        }
        else if (isFloat)
        {
            PlayerPrefs.SetFloat(key, floatValue);
        }
        else if (isString)
        {
            PlayerPrefs.SetString(key, stringValue);
        } else if (isID)
        {
            PlayerPrefs.SetString(key, gameObject.transform.name);
        }
    }
}
