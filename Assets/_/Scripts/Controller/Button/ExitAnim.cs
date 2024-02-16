using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitAnim : BaseButton
{
    public Animator animator;
    public GameObject inactiveObject;

    public void Exit()
    {
        animator.SetTrigger("out");

        // lấy thời gian animation đang chạy bởi animator
        StartCoroutine(CoroutineHelper.DelaySeconds(() => inactiveObject.SetActive(false), animator.GetCurrentAnimatorStateInfo(0).length));
    }

    protected override void OnClick()
    {
        Exit();
    }
}
