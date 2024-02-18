using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Windows;

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

    // Lấy thông tin giftcode
    IEnumerator GetGiftcodeInfo()
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

        bool isExist = false;
        // Lấy thông tin giftcode
        adminGiftcode.GetGiftcode(inputGiftcode.text, giftcodeData =>
        {
            this.giftcodeData = giftcodeData;
            isExist = true;
        });

        // chờ 0.3s trước khi tiếp tục
        while (isExist == false)
        {
            yield return new WaitForSeconds(0.1f);
        }

        CheckGiftcode();
    }

    // Kiểm tra thông tin giftcode
    void CheckGiftcode()
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

    // Hiển thị từng item trong giftcode và thêm hero mới nếu có
    IEnumerator ShowGiftcodeItems(Dictionary<string, object> items)
    {
        foreach (var item in items)
        {
            GameObject itemObject = GameAssets.Instance.GetGameObjectFromId(item.Key);

            // Kiểm tra xem itemObject có tồn tại hay không
            if (itemObject != null)
            {
                ItemAssets itemAttribute = itemObject.GetComponent<ItemAssets>();
                showReward.ShowRewardNotification(itemAttribute.GetIcon(), itemAttribute.GetFrame(), itemAttribute.GetItemName() + " x" + item.Value);

                // Tách chuỗi để lấy heroID
                string[] parts = item.Key.Split('_');
                string heroID = parts[0];

                // Nếu heroID là "Hero", thêm hero mới
                if (heroID == "Hero")
                {
                    HeroUnit hero = itemObject.GetComponent<HeroUnit>();
                    if (hero != null) // Kiểm tra hero không null trước khi thêm vào
                    {
                        // chạy item.Value lần để thêm hero mới
                        for (int i = 0; i < int.Parse(item.Value.ToString()); i++)
                        {
                            StartCoroutine(HeroManager.AddNewHero(hero));
                        }
                    }
                }
            }

            // Chờ một chút trước khi hiển thị item tiếp theo
            yield return new WaitForSeconds(0.3f);
        }
    }


    protected override void OnClick()
    {
        if (inputGiftcode.text.Length <= 0)
        {
            Notification.instance.ShowNotifications("Bạn chưa nhập giftcode");
            return;
        }
        StartCoroutine(GetGiftcodeInfo());
    }
}
