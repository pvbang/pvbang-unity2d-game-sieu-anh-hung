using UnityEngine;

public static class WaitingController
{
    public static void StartWaiting()
    {
        PlayerPrefs.SetInt("ActiveWaiting", 1);
    }

    public static void EndWaiting()
    {
        PlayerPrefs.SetInt("ActiveWaiting", 0);
    }
}
