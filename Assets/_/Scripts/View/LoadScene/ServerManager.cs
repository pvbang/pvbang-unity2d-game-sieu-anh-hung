using Firebase.Auth;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ServerManager : MonoBehaviour
{
    private string serverID;
    private Server server;
    private List<Server> servers;

    private List<Server> serversByAccount;

    private BackgroundController backgroundController;
    private TextMeshProUGUI serverName;
    

    private void Awake()
    {
        if (PlayerPrefs.HasKey("ServerID")) {
            serverID = PlayerPrefs.GetString("ServerID");
        } else
        {
            serverID = "S1";
            PlayerPrefs.SetString("ServerID", serverID);
        }

        backgroundController = GetComponentInChildren<BackgroundController>();
        serverName = GetComponentInChildren<TextMeshProUGUI>();

        server = new Server();
        servers = new List<Server>();
        serversByAccount = new List<Server>();
    }


    private void Start()
    {
        StartCoroutine(GetServer(serverID));
        //CreateManyServer();
    }

    private void FixedUpdate()
    {
        if (!PlayerPrefs.HasKey("ServerID"))
        {
            Debug.Log("ServerID 1: " + PlayerPrefs.GetString("ServerID"));
            serverID = "S1";
            PlayerPrefs.SetString("ServerID", serverID);
            StartCoroutine(GetServer(serverID));
        }

        // nếu PlayerPrefs ServerID thay đổi giá trị thì cập nhật lại server
        if (PlayerPrefs.GetString("ServerID") != serverID)
        {
            serverID = PlayerPrefs.GetString("ServerID");
            StartCoroutine(GetServer(serverID));
        }
    }

    // tạo server mới
    public void CreateNewServer(string id, string username, string status)
    {
        StartCoroutine(_Servers.CreateNewServer(id, username, status));
    }

    // admin test tạo nhiều server
    public void CreateManyServer()
    {
        CreateNewServer("S1", "Siêu Anh Hùng", "FULL");
        CreateNewServer("S2", "Monkey D. Luffy", "FULL");
        CreateNewServer("S3", "Goku", "FULL");
        CreateNewServer("S4", "Naruto Uzumaki", "NEW");
        CreateNewServer("S5", "Sasuke Uchiha", "NEW");
        CreateNewServer("S6", "Itachi Uchiha", "NEW");
        CreateNewServer("S7", "Saitama", "NEW");
        CreateNewServer("S8", "Tanjiro Kamado", "NEW");
        CreateNewServer("S9", "Zenitsu Agatsuma", "NEW");
        CreateNewServer("S9", "Inosuke Hashibira", "NEW");
    }


    // lấy danh sách server
    public IEnumerator GetListServer()
    {
        WaitingController.Instance.StartWaiting();
        var task = FirebaseConnection.instance.databaseReference.Child("servers").GetValueAsync();

        yield return new WaitUntil(() => task.IsCompleted);

        if (task.IsFaulted)
        {
            NotificationGame.instance.ShowNotifications("Lỗi lấy danh sách server");
        }
        else if (task.IsCompleted)
        {
            DataSnapshot snapshot = task.Result;
            this.servers.Clear();

            if (snapshot.Exists)
            {
                foreach (DataSnapshot child in snapshot.Children)
                {
                    Server server = JsonUtility.FromJson<Server>(child.GetRawJsonValue());
                    this.servers.Add(server);
                }
                //Debug.Log("Lấy danh sách server thành công");
                //GetServerFromList(serverID);
            }
            else
            {
                NotificationGame.instance.ShowNotifications("Không có server nào");
            }
        }

        WaitingController.Instance.EndWaiting();
    }

    // lấy danh sách server đã đăng ký bởi tài khoản
    public IEnumerator GetListServerByAccount()
    {
        if (FirebaseAuth.DefaultInstance.CurrentUser == null) yield break;
        if (serverID.Length == 0) yield break;

        WaitingController.Instance.StartWaiting();
        var task = FirebaseConnection.instance.databaseReference.Child("accounts").Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId).Child("servers").GetValueAsync();

        yield return new WaitUntil(() => task.IsCompleted);

        if (task.IsFaulted)
        {
            NotificationGame.instance.ShowNotifications("Lỗi lấy danh sách server từng chơi");
        }
        else if (task.IsCompleted)
        {
            DataSnapshot snapshot = task.Result;
            this.serversByAccount.Clear();

            if (snapshot.Exists)
            {
                foreach (DataSnapshot child in snapshot.Children)
                {
                    if (this.servers != null)
                    {
                        Server server = this.servers.Find(s => s.id == child.Key);
                        if (server != null)
                        {
                            this.serversByAccount.Add(server);
                        }
                    }
                }
            }
            else
            {
                Debug.Log("Chưa từng chơi server nào");
            }
        }

        WaitingController.Instance.EndWaiting();
    }

    // lấy thông tin server
    IEnumerator GetServer(string id)
    {
        WaitingController.Instance.StartWaiting();

        var task = FirebaseConnection.instance.databaseReference.Child("servers").Child(id).GetValueAsync();

        yield return new WaitUntil(() => task.IsCompleted);

        if (task.IsFaulted)
        {
            NotificationGame.instance.ShowNotifications("Lỗi lấy thông tin server");
        }
        else if (task.IsCompleted)
        {
            DataSnapshot snapshot = task.Result;
            if (snapshot.Exists)
            {
                Server server = JsonUtility.FromJson<Server>(snapshot.GetRawJsonValue());
                this.server = server;
                //Debug.Log("Lấy thông tin server thành công");
                SetServerUI(server);
            }
            else
            {
                NotificationGame.instance.ShowNotifications("Server không tồn tại");
            }
        }

        WaitingController.Instance.EndWaiting();
    }


    // lấy thông tin server từ list
    public Server GetServerFromList(string id)
    {
        foreach (Server server in servers)
        {
            if (server.id == id)
            {
                this.server = server;
                SetServerUI(server);
                return server;
            }
        }
        return null;
    }

    // set thông tin giao diện
    public void SetServerUI(Server server)
    {
        if (server != null)
        {
            int status = GetStatus(server.status);
            
            backgroundController.ChangeBackground(status);
            serverName.text = "SAH " + server.id;
        }
    }

    // lấy trạng thái server
    public int GetStatus(string status)
    {
        if (status == "NEW")
        {
            return 0;
        }
        else if (status == "FULL")
        {
            return 1;
        }
        else
        {
            return 2;
        }
    }

    // lấy danh sách server
    public List<Server> GetServers()
    {
        return this.servers;
    }

    // lấy danh sách server đã đăng ký bởi tài khoản
    public List<Server> GetServersByAccount()
    {
        return this.serversByAccount;
    }
}
