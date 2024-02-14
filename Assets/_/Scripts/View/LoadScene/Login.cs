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
        //var email = username + "@gmail.com";
        //FirebaseConnection.instance.auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
        //    if (task.IsCanceled)
        //    {
        //        Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
        //        return;
        //    }
        //    if (task.IsFaulted)
        //    {
        //        Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
        //        return;
        //    }

        //    Firebase.Auth.AuthResult result = task.Result;
        //    Debug.LogFormat("User signed in successfully: {0} ({1})",
        //        result.User.DisplayName, result.User.UserId);
        //});

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

        //var task = FirebaseConnection.instance.databaseReference.Child("accounts").Child(username).GetValueAsync();

        //yield return new WaitUntil(() => task.IsCompleted);

        //if (task.IsFaulted)
        //{
        //    Notification.instance.ShowNotifications("Lỗi tạo tài khoản");
        //}
        //else if (task.IsCompleted)
        //{
        //    DataSnapshot snapshot = task.Result;
        //    if (snapshot.Exists)
        //    {
        //        Account account = JsonUtility.FromJson<Account>(snapshot.GetRawJsonValue());
        //        if (account.password == password)
        //        {
        //            Debug.Log("Đăng nhập thành công: " + username);
        //            PlayerPrefs.SetString("Account", username);
        //            Notification.instance.ShowNotifications("Đăng nhập thành công");
        //            login.SetActive(false);
        //        } else
        //        {
        //            Notification.instance.ShowNotifications("Sai mật khẩu");
        //        }
        //    }
        //    else
        //    {
        //        Notification.instance.ShowNotifications("Tài khoản không tồn tại");
        //    }
        //}
    }
}
