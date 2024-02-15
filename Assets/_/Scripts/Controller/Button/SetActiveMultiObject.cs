using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveMultiObject : BaseButton
{
    public GameObject[] objs;
    public bool[] actives;

    public bool waitAnimation = false;
    public float waitSeconds = 0.0f;

    protected override void OnClick()
    {
        if (waitAnimation)
        {
            // chạy hết objs
            for (int i = 0; i < objs.Length; i++)
            {
                StartCoroutine(CoroutineHelper.DelaySeconds(() => objs[i].SetActive(actives[i]), GetComponent<ButtonAnimation>().GetLengthAnimation()));
            }
        }
        else
        {
            for (int i = 0; i < objs.Length; i++)
            {
                StartCoroutine(CoroutineHelper.DelaySeconds(() => objs[i].SetActive(actives[i]), waitSeconds));
            }
        }
    }

}
