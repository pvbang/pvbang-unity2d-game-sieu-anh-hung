using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Firebase.Database;
using Firebase.Auth;

public class Register : MonoBehaviour
{
    public TMP_InputField usernameText;
    public TMP_InputField passwordText;
    public TMP_InputField confirmPasswordText;

    public GameObject register;

    private void Awake()
    {
        register = transform.parent.parent.gameObject;
    }

    public void RegisterButton()
    {
        // không được để trống
        if ((usernameText.text.Length <= 0) | (passwordText.text.Length <= 0) && (confirmPasswordText.text.Length <= 0))
        {
            Notification.instance.ShowNotifications("Bạn cần điền đủ thông tin");
            return;
        }

        // username phải từ 4-30 ký tự
        if ((usernameText.text.Length < 4) | (usernameText.text.Length > 30))
        {
            Notification.instance.ShowNotifications("Tên đăng nhập phải từ 4-30 ký tự");
            return;
        }

        // mật khẩu phải từ 6-20 ký tự
        if ((passwordText.text.Length < 6) | (passwordText.text.Length > 20))
        {
            Notification.instance.ShowNotifications("Mật khẩu phải từ 6-20 ký tự");
            return;
        }

        // mật khẩu phải giống nhau
        if (passwordText.text != confirmPasswordText.text)
        {
            Notification.instance.ShowNotifications("Mật khẩu phải giống nhau");
            return;
        }

        StartCoroutine(CreateNewUser(usernameText.text, passwordText.text));
    }

    // tạo tài khoản mới
    IEnumerator CreateNewUser(string username, string password)
    {
        WaitingController.Instance.StartWaiting();
        var email = username + "@gmail.com";

        var task = FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(email, password);
        yield return new WaitUntil(() => task.IsCompleted);

        if (task.IsFaulted)
        {
            if (task.Exception.Message.Contains("The email address is already in use by another account"))
            {
                Notification.instance.ShowNotifications("Tên tài khoản đã tồn tại");
            }
            else
            {
                Notification.instance.ShowNotifications("Lỗi tạo tài khoản");
            }
        }
        else if (task.IsCompleted)
        {
            AuthResult result = task.Result;
            Dictionary<string, object> userID = new Dictionary<string, object>
            {
                { "userID", result.User.UserId }
            };

            FirebaseConnection.instance.databaseReference.Child("accounts").Child(result.User.UserId).SetValueAsync(userID);
            Notification.instance.ShowNotifications("Tạo tài khoản thành công");
            register.SetActive(false);
        }
        WaitingController.Instance.EndWaiting();
    }
}