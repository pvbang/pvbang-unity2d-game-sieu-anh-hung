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
        Exit();
    }

    private void Awake()
    {
        ListServer = GameObject.Find("ListServer");
        textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Exit()
    {
        Animator animator = ListServer.GetComponent<Animator>();
        if (animator == null)
        {
            ListServer.SetActive(false);
            return;
        }

        animator.SetTrigger("out");

        // lấy thời gian animation đang chạy bởi animator
        StartCoroutine(CoroutineHelper.DelaySeconds(() => ListServer.SetActive(false), animator.GetCurrentAnimatorStateInfo(0).length));
    }
}
