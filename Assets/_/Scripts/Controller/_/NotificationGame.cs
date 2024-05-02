using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
    
// NotificationGame.instance.ShowNotifications("Thông báo");

public class NotificationGame : MonoBehaviour
{
    public static NotificationGame instance;
    public GameObject notifications;
    public TextMeshProUGUI notificationsTextMeshProUGUI;

    private void Awake()
    {
        NotificationGame.instance = this;
    }

    // Hiển thị thông báo
    public void ShowNotifications(string text)
    {
        notifications.SetActive(true);
        notificationsTextMeshProUGUI.text = text;
    }
}
