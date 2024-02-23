using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelfereContent : MonoBehaviour
{
    public GameObject[] contents;
    public GameObject[] frames;

    private void Awake()
    {
        SetActiveChoiceObject(0);
    }

    private void OnEnable()
    {
        SetActiveChoiceObject(0);
    }

    public void SetActiveChoiceObject(int index)
    {
        for (int i = 0; i < contents.Length; i++)
        {
            contents[i].SetActive(i == index);
            frames[i].SetActive(i == index);
        }
    }
}
