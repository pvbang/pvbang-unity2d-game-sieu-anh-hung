using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ListServer : MonoBehaviour
{
    private string username;
    private ServerManager serverManager;

    public Transform item;
    public Transform itemBlank;

    private Content contentAccountServer;
    private Content contentAllServer;

    private List<Server> servers;
    private List<Server> serversByAccount;

    private void Awake()
    {
        username = PlayerPrefs.GetString("Account");
        serverManager = GetComponentInParent<ServerManager>();
        contentAllServer = GameObject.Find("AllServer").GetComponentInChildren<Content>();
        contentAccountServer = GameObject.Find("AcountServer").GetComponentInChildren<Content>();  
    }

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return StartCoroutine(serverManager.GetListServer());
        yield return StartCoroutine(serverManager.GetListServerByAccount());

        servers = serverManager.GetServers();
        if (servers != null)
        {
            servers.Reverse();
            ShowListAllServers(servers);
        }

        serversByAccount = serverManager.GetServersByAccount();
        if (serversByAccount != null)
        {
            serversByAccount.Reverse();
            ShowListAccountServers(serversByAccount);
        }
    }

    void OnEnable()
    {
        if (PlayerPrefs.GetString("Account") != username)
        {
            username = PlayerPrefs.GetString("Account");
            StartCoroutine(LoadScene());
        }
    }

    // hiển thị danh sách các servers đã chơi
    public void ShowListAccountServers(List<Server> servers)
    {
        if (servers.Count == 0)
        {
            Transform itemTransform = Instantiate(this.itemBlank);
            contentAccountServer.DestroyContents();
            contentAccountServer.AddContent(itemTransform);
            return;
        }

        List<Transform> contentList = new List<Transform>();
        foreach (Server server in servers)
        {
            Transform itemTransform = Instantiate(this.item);
            itemTransform.GetComponentInChildren<TextMeshProUGUI>().text = server.id + " - " + server.name;
            itemTransform.GetComponentInChildren<BackgroundController>().ChangeBackground(serverManager.GetStatus(server.status));
            contentList.Add(itemTransform);
        }

        contentAccountServer.DestroyContents();
        contentAccountServer.AddContent(contentList);
    }

    // hiển thị danh sách tất cả servers
    public void ShowListAllServers(List<Server> servers)
    {
        List<Transform> contentList = new List<Transform>();
        foreach (Server server in servers)
        {
            Transform itemTransform = Instantiate(this.item);
            itemTransform.GetComponentInChildren<TextMeshProUGUI>().text = server.id + " - " + server.name;
            itemTransform.GetComponentInChildren<BackgroundController>().ChangeBackground(serverManager.GetStatus(server.status));
            contentList.Add(itemTransform);
        }

        contentAllServer.DestroyContents();
        contentAllServer.AddContent(contentList);
    }

    // đảo ngược danh sách tất cả servers
    public void InvertListServer()
    {
        servers.Reverse();
        ShowListAllServers(servers);
    }

    // đảo ngược danh sách servers đã chơi
    public void RevertAccountServer()
    {
        serversByAccount.Reverse();
        ShowListAccountServers(serversByAccount);
    }
}
