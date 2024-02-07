using Firebase.Database;
using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Mail;

public class SetupLoadScene : MonoBehaviour
{
    private FirebaseUser user;
    private string serverID;

    public GameObject Server;
    public GameObject CreateAccount;
    public GameObject ClickLogin;
    public GameObject StartGame;
    public GameObject Account;

    private bool createAccount = false;
    private bool startGame = false;

    private void Awake()
    {
        user = FirebaseConnection.instance.auth.CurrentUser;

        serverID = PlayerPrefs.GetString("ServerID");
        SetActive(false, false, false, false, false);
    }

    void Start()
    {
        StartCoroutine(GetInfoAccount());
    }

    private void FixedUpdate()
    {
        if (PlayerPrefs.GetString("ServerID") != serverID)
        {
            serverID = PlayerPrefs.GetString("ServerID");
            StartCoroutine(GetInfoAccount());
        }

        if (FirebaseConnection.instance.auth.CurrentUser != user)
        {
            user = FirebaseConnection.instance.auth.CurrentUser;
            StartCoroutine(GetInfoAccount());
        }

        if (user != null)
        {
            if (startGame)
            {
                SetActive(true, false, false, true, true);
            }
            else if (createAccount)
            {
                SetActive(true, true, false, false, true);
            }
        }
        else
        {
            SetActive(false, false, true, false, false);
        }
    }

    public void SetActive(bool server, bool createAccount, bool clickLogin, bool startGame, bool account)
    {
        Server.SetActive(server);
        CreateAccount.SetActive(createAccount);
        ClickLogin.SetActive(clickLogin);
        StartGame.SetActive(startGame);
        Account.SetActive(account);
    }

    // lấy thông tin tài khoản
    public IEnumerator GetInfoAccount()
    {
        if (FirebaseConnection.instance.auth.CurrentUser == null) yield break;
        if (serverID.Length == 0) yield break;

        var task = FirebaseConnection.instance.databaseReference.Child("accounts").Child(user.UserId).Child("servers").Child(serverID).GetValueAsync();

        yield return new WaitUntil(() => task.IsCompleted);

        if (task.IsFaulted)
        {
            Notification.instance.ShowNotifications("Lỗi kết nối");
        }
        else if (task.IsCompleted)
        {
            DataSnapshot snapshot = task.Result;
            if (snapshot.Exists)
            {
                startGame = true;
                createAccount = false;

                // Debug.Log("Đã chơi server này");
            }
            else
            {
                createAccount = true;
                startGame = false;

                // Debug.Log("Chưa chơi server này");
            }
        }
    }
}
