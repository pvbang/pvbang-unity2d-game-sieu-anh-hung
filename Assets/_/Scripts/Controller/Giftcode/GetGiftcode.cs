using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetGiftcode : BaseButton
{
    public TMP_InputField inputGiftcode;
    private GiftcodeManager adminGiftcode;
    private ShowReward showReward;

    private Dictionary<string, object> giftcodeData;

    private void Awake()
    {
        adminGiftcode = GetComponent<GiftcodeManager>();
        showReward = GetComponent<ShowReward>();
    }

    IEnumerator GetGiftcodeButton()
    {
        bool isUsed = false;

        // Kiểm tra giftcode đã được sử dụng chưa
        adminGiftcode.CheckGiftcodeUsed(inputGiftcode.text, isU =>
        {
            isUsed = isU;
        });

        // chờ 0.3s trước khi tiếp tục
        yield return new WaitForSeconds(0.3f);

        // Nếu giftcode đã được sử dụng thì không cần kiểm tra tiếp
        if (isUsed)
        {
            Notification.instance.ShowNotifications("Giftcode đã được sử dụng");
            yield break;
        }

        // Lấy thông tin giftcode
        adminGiftcode.GetGiftcode(inputGiftcode.text, giftcodeData =>
        {
            this.giftcodeData = giftcodeData;
        });

        // chờ 0.3s trước khi tiếp tục
        yield return new WaitForSeconds(0.3f);

        ShowGiftcodeReward();
    }

    IEnumerator ShowGiftcodeItems(Dictionary<string, object> items)
    {
        foreach (var item in items)
        {
            Debug.Log("Key: " + item.Key + ", Value: " + item.Value);

            GameObject itemObject = GameAssets.Instance.GetGameObjectFromId(item.Key);

            if (itemObject != null)
            {
                ItemAssets itemAttribute = itemObject.GetComponent<ItemAssets>();
                showReward.ShowRewardNotification(itemAttribute.GetImage(), itemAttribute.GetFrame(), itemAttribute.GetIconName() + " x" + item.Value);
            }

            // Chờ một chút trước khi hiển thị item tiếp theo
            yield return new WaitForSeconds(0.3f);
        }
    }

    void ShowGiftcodeReward()
    {
        if (giftcodeData == null)
        {
            Notification.instance.ShowNotifications("Giftcode không tồn tại");
            return;
        }

        // kiểm tra thời gian hiện tại có nằm trong thời gian sử dụng giftcode không
        if (adminGiftcode.CheckGiftcodeTime((string)giftcodeData["timestamp"]) == false)
        {
            Notification.instance.ShowNotifications("Giftcode đã hết hạn");
            return;
        }

        adminGiftcode.AddGiftcodeToUser(inputGiftcode.text, giftcodeData, isAdd =>
        {
            if (!isAdd)
            {
                Notification.instance.ShowNotifications("Nhận giftcode thất bại");
            }
        });

        // Gọi coroutine để hiển thị từng item một
        StartCoroutine(ShowGiftcodeItems((Dictionary<string, object>)giftcodeData["items"]));
    }


    protected override void OnClick()
    {
        if (inputGiftcode.text.Length <= 0)
        {
            Notification.instance.ShowNotifications("Bạn chưa nhập giftcode");
            return;
        }
        StartCoroutine(GetGiftcodeButton());
    }
}
