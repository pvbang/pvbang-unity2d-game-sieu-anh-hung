using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnimation : BaseButton
{
    private Animation anim;
    private string nameAnimation = "BaseButton";

    protected override void OnClick()
    {
        PlayAnimation();
    }

    // hàm này sẽ chạy animation
    public void PlayAnimation()
    {
        anim.Play(nameAnimation);
    }

    private void Awake()
    {
        anim = GetComponent<Animation>();
        if (!anim)
        {
            anim = GetComponentInChildren<Animation>();
        }
    }

    // hàm này sẽ lấy tên của animation
    public string GetNameAnimation()
    {
        return nameAnimation;
    }

    // hàm này sẽ lấy độ dài của animation
    public float GetLengthAnimation()
    {
        return anim.GetClip(nameAnimation).length;
    }
}
