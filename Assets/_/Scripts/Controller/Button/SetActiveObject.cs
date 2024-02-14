using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveObject : BaseButton
{
    public GameObject obj;
    public bool isObjectParent = false;

    public bool active = false;
    public bool waitAnimation = false;

    protected override void OnClick()
    {
        if (waitAnimation)
        {
            StartCoroutine(CoroutineHelper.DelaySeconds(() => obj.SetActive(active), GetComponent<ButtonAnimation>().GetLengthAnimation()));
        }
        else
        {
            obj.SetActive(active);
        }
    }

    private void Awake()
    {
        if (isObjectParent)
        {
            obj = transform.parent.gameObject;
        }
        if (obj == null)
        {
            obj = gameObject;
        }
    }
}
