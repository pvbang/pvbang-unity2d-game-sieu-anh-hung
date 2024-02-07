using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerPrefs : BaseButton
{
    public bool isInt = false;
    public bool isFloat = false;
    public bool isString = false;

    public int intValue;
    public float floatValue;
    public string stringValue;

    protected override void OnClick()
    {
        if (isInt)
        {
            PlayerPrefs.SetInt("Int", intValue);
        }
        else if (isFloat)
        {
            PlayerPrefs.SetFloat("Float", floatValue);
        }
        else if (isString)
        {
            PlayerPrefs.SetString("String", stringValue);
        }
    }
}
