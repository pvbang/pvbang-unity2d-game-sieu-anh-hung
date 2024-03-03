using Firebase.Auth;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILoadScene : MonoBehaviour
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
        user = FirebaseAuth.DefaultInstance.CurrentUser;

        serverID = PlayerPrefs.GetString("ServerID");
        SetActive(false, false, false, false, false);
    }

    void Start()
    {
        StartCoroutine(GetInfoAccount());
    }

    void OnEnable()
    {
        user = FirebaseAuth.DefaultInstance.CurrentUser;
        StartCoroutine(GetInfoAccount());
    }

    private void FixedUpdate()
    {
        if (PlayerPrefs.HasKey("ServerID"))
        {
            if (PlayerPrefs.GetString("ServerID") != serverID)
            {
                serverID = PlayerPrefs.GetString("ServerID");
                StartCoroutine(GetInfoAccount());
            }
        }

        if (serverID.Length == 0)
        {
            serverID = "S1";
            PlayerPrefs.SetString("ServerID", serverID);
        }

        if (user != null)
        {
            if ((FirebaseAuth.DefaultInstance.CurrentUser != user))
            {
                user = FirebaseAuth.DefaultInstance.CurrentUser;
                if (!PlayerPrefs.HasKey("ServerID"))
                {
                    serverID = "S1";
                    PlayerPrefs.SetString("ServerID", serverID);
                }
                StartCoroutine(GetInfoAccount());
            }

            if (startGame)
            {
                SetActive(true, false, false, true, true);
            }
            else if (createAccount)
            {
                SetActive(true, true, false, false, true);
            } else
            {
                StartCoroutine(GetInfoAccount());
            }
        }
        else
        {
            SetActive(false, false, true, false, false);
            if (FirebaseAuth.DefaultInstance.CurrentUser != null)
            {
                user = FirebaseAuth.DefaultInstance.CurrentUser;
                StartCoroutine(GetInfoAccount());
            }
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
        if (FirebaseAuth.DefaultInstance.CurrentUser == null) yield break;
        if (serverID.Length == 0) yield break;

        WaitingController.Instance.StartWaiting();

        var task = FirebaseConnection.instance.databaseReference.Child("accounts").Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId).Child("servers").Child(serverID).GetValueAsync();

        yield return new WaitUntil(() => task.IsCompleted);

        if (task.IsFaulted)
        {
            Notification.instance.ShowNotifications("Lỗi kết nối đến server");
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

        WaitingController.Instance.EndWaiting();
    }
}
