using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


// item của list server
public class ChooseServer : BaseButton
{
    private GameObject ListServer;
    private TextMeshProUGUI textMeshProUGUI;

    protected override void OnClick()
    {
        // lấy id server
        string input = textMeshProUGUI.text;
        string[] parts = input.Split(" - ");
        string result = parts[0];

        PlayerPrefs.SetString("ServerID", result);

        if (ListServer == null) return;
        ListServer.SetActive(false);
    }

    private void Awake()
    {
        ListServer = GameObject.Find("ListServer");
        textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
    }
}
