using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveObject : BaseButton
{
    public GameObject obj;
    public bool isObjectParent = false;

    public bool active = false;
    public bool waitAnimation = false;

    public bool waitSecondsRandom = false;
    public float waitSeconds = 0.0f;

    public bool reverseActive = false;

    public bool onEnable = false;

    protected override void OnClick()
    {
        if (obj.activeSelf && reverseActive)
        {
            active = false;
        }
        else if (!obj.activeSelf && reverseActive)
        {
            active = true;
        }

        if (waitAnimation)
        {
            StartCoroutine(CoroutineHelper.DelaySeconds(() => obj.SetActive(active), GetComponent<ButtonAnimation>().GetLengthAnimation()));
        }
        else
        {
            if (waitSecondsRandom)
            {
                waitSeconds = Random.Range(0.0f, waitSeconds);
            }
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

    private void OnEnable()
    {
        if (onEnable)
        {
            if (isObjectParent)
            {
                obj = transform.parent.gameObject;
            }
            if (obj == null)
            {
                obj = gameObject;
            }

            if (obj.activeSelf && reverseActive)
            {
                active = false;
            }
            else if (!obj.activeSelf && reverseActive)
            {
                active = true;
            }

            if (waitAnimation)
            {
                StartCoroutine(CoroutineHelper.DelaySeconds(() => obj.SetActive(active), GetComponent<ButtonAnimation>().GetLengthAnimation()));
            }
            else
            {
                if (waitSecondsRandom)
                {
                    waitSeconds = Random.Range(0.0f, waitSeconds);
                }
                StartCoroutine(CoroutineHelper.DelaySeconds(() => obj.SetActive(active), waitSeconds));
            }
        }
    }

    private void OnDisable()
    {
        if (obj)
        {
            obj.SetActive(false);
        }
    }
}
