using System.Collections;
using UnityEngine;

// WaitingController.Instance.

public class WaitingController : MonoBehaviour
{
    private static WaitingController _instance;
    public static WaitingController Instance => _instance;

    private void Awake()
    {
        WaitingController._instance = this;
    }

    public void StartWaiting()
    {
        PlayerPrefs.SetInt("ActiveWaiting", 1);
    }

    public void EndWaiting()
    {
        PlayerPrefs.SetInt("ActiveWaiting", 0);
    }

    public void WaitingSeconds(float seconds)
    {
        StartWaiting();
        CoroutineHelper.DelaySeconds(() => EndWaiting(), seconds);
    }

    public void DelaySeconds(float seconds)
    {
        StartWaiting();
        StartCoroutine(CoroutineHelper.DelaySeconds(() => EndWaiting(), seconds));
    }
}
