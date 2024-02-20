using Firebase.Auth;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    public TMP_InputField usernameText;
    public TMP_InputField passwordText;

    public GameObject login;

    private void Awake()
    {
        login = transform.parent.parent.gameObject;
    }

    // kiểm tra thông tin đăng nhập
    public void LoginButton()
    {
        // không được để trống
        if ((usernameText.text.Length <= 0) | (passwordText.text.Length <= 0))
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

        StartCoroutine(CheckUserLogin(usernameText.text, passwordText.text));
    }

    // đăng nhập
    IEnumerator CheckUserLogin(string username, string password)
    {
        WaitingController.StartWaiting();
        var email = username + "@gmail.com";

        var task = FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(email, password);
        yield return new WaitUntil(() => task.IsCompleted);

        if (task.IsFaulted)
        {
            Notification.instance.ShowNotifications("Sai tài khoản hoặc mật khẩu");
        }
        else if (task.IsCompleted)
        {
            AuthResult result = task.Result;
            Debug.Log("Đăng nhập thành công: " + result.User.UserId);
            Notification.instance.ShowNotifications("Đăng nhập thành công");
            login.SetActive(false);
        }
        WaitingController.EndWaiting();
    }
}
