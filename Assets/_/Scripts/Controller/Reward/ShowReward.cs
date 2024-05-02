using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ShowReward.instance.ShowRewardNotification(itemAttribute.GetIcon(), itemAttribute.GetFrame(), itemAttribute.GetItemName() + " x" + item.Value);
public class ShowReward : MonoBehaviour
{
    public static ShowReward instance;
    public GameObject reward;

    private void Awake()
    {
        ShowReward.instance = this;
    }

    private void Start()
    {
        if (reward == null)
        {
            reward = GameAssets.Instance.GetGameObjectFromId("Item_Reward");
        }
    }

    public void ShowRewardNotification(Sprite sprite, Sprite frame, string rewardText)
    {
        if (reward == null)
        {
            reward = GameAssets.Instance.GetGameObjectFromId("Item_Reward");
            GameObject initReward = Instantiate(reward, new Vector3(0, 0, 0), Quaternion.identity);
            initReward.GetComponent<RewardController>().SetReward(sprite, frame, rewardText);
        }
        else
        {
            GameObject initReward = Instantiate(reward, new Vector3(0, 0, 0), Quaternion.identity);
            initReward.GetComponent<RewardController>().SetReward(sprite, frame, rewardText);
        }
    }

}
