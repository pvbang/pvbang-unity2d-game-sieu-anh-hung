using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelfereContent : MonoBehaviour
{
    public GameObject[] contents;

    public void SetActiveChoiceObject(int index)
    {
        for (int i = 0; i < contents.Length; i++)
        {
            contents[i].SetActive(i == index);
        }
    }
}
