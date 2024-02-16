using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowReward : MonoBehaviour
{
    public GameObject reward;
    private GameObject initReward;

    private void Start()
    {
        if (reward == null)
        {
            reward = GameAssets.Instance.GetGameObjectFromId("reward");
        }
    }

    public void ShowRewardNotification(Image sprite, Image frame, string rewardText)
    {
        initReward = Instantiate(reward, new Vector3(0, 0, 0), Quaternion.identity);
        initReward.GetComponent<RewardController>().SetReward(sprite, frame, rewardText);
    }

}
