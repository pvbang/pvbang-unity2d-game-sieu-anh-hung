using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWaiting : MonoBehaviour
{
    public GameObject waiting;
    private int activeWaiting = 0;

    private void Awake()
    {
        activeWaiting = 0;
        PlayerPrefs.SetInt("ActiveWaiting", 0);
    }

    private void FixedUpdate()
    {
        int newActiveWaiting = PlayerPrefs.GetInt("ActiveWaiting");
        if (newActiveWaiting != activeWaiting)
        {
            activeWaiting = newActiveWaiting;
            waiting.SetActive(activeWaiting == 1);
        }
    }
}
