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

    private Button _button;

    private Dictionary<string, object> giftcodeData;

    private void Awake()
    {
        adminGiftcode = GetComponent<GiftcodeManager>();
        showReward = GetComponent<ShowReward>();

        _button = GetComponent<Button>();
    }

    // Lấy thông tin giftcode
    IEnumerator GetGiftcodeInfo()
    {
        _button.interactable = false;
        bool isUsed = false;

        bool isExistUsedCheck = false;
        // Kiểm tra giftcode đã được sử dụng chưa
        adminGiftcode.CheckGiftcodeUsed(inputGiftcode.text, isU =>
        {
            isUsed = isU;
            isExistUsedCheck = true;
        });

        while (isExistUsedCheck == false)
        {
            yield return new WaitForSeconds(0.1f);
        }

        // Nếu giftcode đã được sử dụng thì không cần kiểm tra tiếp
        if (isUsed)
        {
            NotificationGame.instance.ShowNotifications("Giftcode này đã được sử dụng");
            _button.interactable = true;
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
            NotificationGame.instance.ShowNotifications("Giftcode này không tồn tại");
            _button.interactable = true;
            return;
        }

        // kiểm tra thời gian hiện tại có nằm trong thời gian sử dụng giftcode không
        if (adminGiftcode.CheckGiftcodeTime((string)giftcodeData["timestamp"]) == false)
        {
            NotificationGame.instance.ShowNotifications("Giftcode này đã hết hạn");
            _button.interactable = true;
            return;
        }

        adminGiftcode.AddGiftcodeToUser(inputGiftcode.text, giftcodeData, isAdd =>
        {
            if (!isAdd)
            {
                NotificationGame.instance.ShowNotifications("Nhận giftcode thất bại");
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
                // Nếu heroID là "CS", thêm item mới
                else if (heroID == "CS")
                {
                    // chạy item.Value lần
                    for (int i = 0; i < int.Parse(item.Value.ToString()); i++)
                    {
                        StartCoroutine(ItemManager.AddNewItem(item.Key, int.Parse(item.Value.ToString())));
                    }
                }
            }

            // Chờ một chút trước khi hiển thị item tiếp theo
            yield return new WaitForSeconds(0.3f);
        }

        _button.interactable = true;
    }


    protected override void OnClick()
    {
        if (inputGiftcode.text.Length <= 0)
        {
            NotificationGame.instance.ShowNotifications("Bạn chưa nhập giftcode");
            return;
        }
        StartCoroutine(GetGiftcodeInfo());
    }
}
