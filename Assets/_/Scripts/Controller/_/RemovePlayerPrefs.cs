using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovePlayerPrefs : BaseButton
{
    public bool removeAllPlayerPrefs = false;
    public string PlayerPrefsKey;

    // xóa player pref
    public void RemovePlayerPref()
    {
        if (removeAllPlayerPrefs)
        {
            PlayerPrefs.DeleteAll();
        }
        else
        {
            PlayerPrefs.DeleteKey(PlayerPrefsKey);
        }
    }

    protected override void OnClick()
    {
        if (GetComponent<ButtonAnimation>())
        {
            StartCoroutine(CoroutineHelper.DelaySeconds(() => RemovePlayerPref(), GetComponent<ButtonAnimation>().GetLengthAnimation()));
        } else
        {
            RemovePlayerPref();
        }
        
    }
}
