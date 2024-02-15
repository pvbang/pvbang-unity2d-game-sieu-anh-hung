using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetReward : MonoBehaviour
{
    public GameObject reward;

    private void Start()
    {
        if (reward == null)
        {
            reward = GameAssets.Instance.GetGameObjectFromId("reward");
        }
    }

    public void ShowRewardNotification()
    {
        Instantiate(reward, new Vector3(0,0,0), Quaternion.identity);
    }
}
