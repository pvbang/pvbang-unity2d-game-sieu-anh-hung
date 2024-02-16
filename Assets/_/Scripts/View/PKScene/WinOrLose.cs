using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinOrLose : MonoBehaviour
{
    public bool isWin = true;
    public GameObject win;
    public GameObject lose;

    public void ShowWin()
    {
        win.SetActive(true);
        lose.SetActive(false);
    }

    public void ShowLose()
    {
        win.SetActive(false);
        lose.SetActive(true);
    }

    public void ShowWinOrLose(bool isWin)
    {
        if (isWin)
        {
            ShowWin();
        }
        else
        {
            ShowLose();
        }
    }

    private void Awake()
    {
        if (win == null)
        {
            win = transform.Find("Win").gameObject;
        }
        if (lose == null)
        {
            lose = transform.Find("Lost").gameObject;
        }
    }

    private void Start()
    {
        if (win == null || lose == null)
        {
            Debug.Log("Win hoặc Lost object null");
            return;
        }

        ShowWinOrLose(this.isWin);
    }
}
