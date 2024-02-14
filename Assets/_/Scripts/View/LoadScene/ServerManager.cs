using Firebase.Auth;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ServerManager : MonoBehaviour
{
    private FirebaseUser user;

    private string serverID;
    private Server server;
    private List<Server> servers;

    private List<Server> serversByAccount;

    private BackgroundController backgroundController;
    private TextMeshProUGUI serverName;
    

    private void Awake()
    {
        user = FirebaseAuth.DefaultInstance.CurrentUser;
        
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

        //StartCoroutine(CreateNewServer("S1", "Siêu Anh Hùng", "FULL"));
        //StartCoroutine(CreateNewServer("S2", "Monkey D. Luffy", "FULL"));
        //StartCoroutine(CreateNewServer("S3", "Goku", "FULL"));
        //StartCoroutine(CreateNewServer("S4", "Naruto Uzumaki", "NEW"));
        //StartCoroutine(CreateNewServer("S5", "Sasuke Uchiha", "NEW"));
        //StartCoroutine(CreateNewServer("S6", "Itachi Uchiha", "NEW"));
        //StartCoroutine(CreateNewServer("S7", "Saitama", "NEW"));
        //StartCoroutine(CreateNewServer("S8", "Tanjiro Kamado", "NEW"));
        //StartCoroutine(CreateNewServer("S9", "Zenitsu Agatsuma", "NEW"));
        //StartCoroutine(CreateNewServer("S9", "Inosuke Hashibira", "NEW"));

        // StartCoroutine(DeleteServer("S1"));
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

        // nếu PlayerPrefs Account thay đổi giá trị thì cập nhật lại username
        if (FirebaseAuth.DefaultInstance.CurrentUser != user)
        {
            user = FirebaseAuth.DefaultInstance.CurrentUser;
        }
    }

    // tạo server mới
    IEnumerator CreateNewServer(string id, string username, string status)
    {
        var task = FirebaseConnection.instance.databaseReference.Child("servers").Child(id).GetValueAsync();

        yield return new WaitUntil(() => task.IsCompleted);

        if (task.IsFaulted)
        {
            Notification.instance.ShowNotifications("Lỗi tạo server");
        }
        else if (task.IsCompleted)
        {
            DataSnapshot snapshot = task.Result;
            if (snapshot.Exists)
            {
                Notification.instance.ShowNotifications("Tên server đã tồn tại");
            }
            else
            {
                Server server = new Server(id, username, status);
                string json = JsonUtility.ToJson(server);

                FirebaseConnection.instance.databaseReference.Child("servers").Child(id).SetRawJsonValueAsync(json);

                Notification.instance.ShowNotifications("Tạo server thành công");
            }
        }
    }

    // xóa server
    IEnumerator DeleteServer(string id)
    {
        var task = FirebaseConnection.instance.databaseReference.Child("servers").Child(id).GetValueAsync();

        yield return new WaitUntil(() => task.IsCompleted);

        if (task.IsFaulted)
        {
            Notification.instance.ShowNotifications("Lỗi xóa server");
        }
        else if (task.IsCompleted)
        {
            DataSnapshot snapshot = task.Result;
            if (snapshot.Exists)
            {
                FirebaseConnection.instance.databaseReference.Child("servers").Child(id).RemoveValueAsync();

                Notification.instance.ShowNotifications("Xóa server thành công");
            }
            else
            {
                Notification.instance.ShowNotifications("Server không tồn tại");
            }
        }
    }

    // lấy danh sách server
    public IEnumerator GetListServer()
    {
        var task = FirebaseConnection.instance.databaseReference.Child("servers").GetValueAsync();

        yield return new WaitUntil(() => task.IsCompleted);

        if (task.IsFaulted)
        {
            Notification.instance.ShowNotifications("Lỗi lấy danh sách server");
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
                Notification.instance.ShowNotifications("Không có server nào");
            }
        }
    }

    // lấy danh sách server đã đăng ký bởi tài khoản
    public IEnumerator GetListServerByAccount()
    {
        if (FirebaseAuth.DefaultInstance.CurrentUser == null) yield break;
        if (serverID.Length == 0) yield break;

        var task = FirebaseConnection.instance.databaseReference.Child("accounts").Child(user.UserId).Child("servers").GetValueAsync();

        yield return new WaitUntil(() => task.IsCompleted);

        if (task.IsFaulted)
        {
            Notification.instance.ShowNotifications("Lỗi lấy danh sách server từng chơi");
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
    }

    // lấy thông tin server
    IEnumerator GetServer(string id)
    {
        var task = FirebaseConnection.instance.databaseReference.Child("servers").Child(id).GetValueAsync();

        yield return new WaitUntil(() => task.IsCompleted);

        if (task.IsFaulted)
        {
            Notification.instance.ShowNotifications("Lỗi lấy thông tin server");
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
                Notification.instance.ShowNotifications("Server không tồn tại");
            }
        }
    }

    // cập nhật thông tin server
    IEnumerator UpdateServer(string id, string username, string status)
    {
        var task = FirebaseConnection.instance.databaseReference.Child("servers").Child(id).GetValueAsync();

        yield return new WaitUntil(() => task.IsCompleted);

        if (task.IsFaulted)
        {
            Notification.instance.ShowNotifications("Lỗi cập nhật thông tin server");
        }
        else if (task.IsCompleted)
        {
            DataSnapshot snapshot = task.Result;
            if (snapshot.Exists)
            {
                Server server = new Server(id, username, status);
                string json = JsonUtility.ToJson(server);

                FirebaseConnection.instance.databaseReference.Child("servers").Child(id).SetRawJsonValueAsync(json);

                Notification.instance.ShowNotifications("Cập nhật thông tin server thành công");
            }
            else
            {
                Notification.instance.ShowNotifications("Server không tồn tại");
            }
        }
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
        else 
        {
            return 1;
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
