using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveObject : BaseButton
{
    public GameObject obj;
    public bool isObjectParent = false;

    public bool active = false;
    public bool waitAnimation = false;

    public float waitSeconds = 0.0f;

    protected override void OnClick()
    {
        if (waitAnimation)
        {
            StartCoroutine(CoroutineHelper.DelaySeconds(() => obj.SetActive(active), GetComponent<ButtonAnimation>().GetLengthAnimation()));
        }
        else
        {
            StartCoroutine(CoroutineHelper.DelaySeconds(() => obj.SetActive(active), waitSeconds));
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
