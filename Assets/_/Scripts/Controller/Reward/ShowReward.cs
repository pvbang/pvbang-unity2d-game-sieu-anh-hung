using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowReward : MonoBehaviour
{
    public GameObject reward;

    private void Start()
    {
        if (reward == null)
        {
            reward = GameAssets.Instance.GetGameObjectFromId("Item_Reward");
        }
    }

    public void ShowRewardNotification(Sprite sprite, Sprite frame, string rewardText)
    {
        GameObject initReward = Instantiate(reward, new Vector3(0, 0, 0), Quaternion.identity);
        initReward.GetComponent<RewardController>().SetReward(sprite, frame, rewardText);
    }

}
