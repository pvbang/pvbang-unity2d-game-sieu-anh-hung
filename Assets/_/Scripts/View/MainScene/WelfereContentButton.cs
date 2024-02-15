using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelfereContentButton : BaseButton
{
    public int index;
    private WelfereContent welfereContent;

    protected override void OnClick()
    {
        if (welfereContent == null)
        {
            welfereContent = GetComponentInParent<WelfereContent>();
        }

        welfereContent.SetActiveChoiceObject(index);
    }

    private void Awake()
    {
        welfereContent = GetComponentInParent<WelfereContent>();
    }
}
