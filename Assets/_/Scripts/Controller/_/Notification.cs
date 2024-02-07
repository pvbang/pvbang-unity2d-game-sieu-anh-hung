using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
    
// Notification.instance.ShowNotifications("Thông báo");

public class Notification : MonoBehaviour
{
    public static Notification instance;
    public GameObject notifications;
    public TextMeshProUGUI notificationsTextMeshProUGUI;

    private void Awake()
    {
        Notification.instance = this;
    }

    // Hiển thị thông báo
    public void ShowNotifications(string text)
    {
        notifications.SetActive(true);
        notificationsTextMeshProUGUI.text = text;
    }
}
