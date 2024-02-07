using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertListServer : BaseButton
{
    public bool isAllServer = true;
    public bool isAccountServer = false;

    private ListServer listServer;

    protected override void OnClick()
    {
        // đảo ngược list server
        if (isAccountServer)
        {
            listServer.RevertAccountServer();
        }
        else if (isAllServer)
        {
            listServer.InvertListServer();
        }
    }

    private void Awake()
    {
        listServer = GetComponentInParent<ListServer>();
    }

}
