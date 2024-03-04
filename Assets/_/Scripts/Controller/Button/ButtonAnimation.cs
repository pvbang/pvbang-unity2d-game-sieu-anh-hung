using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnimation : BaseButton
{
    private Animation anim;
    public string nameAnimation = "BaseButton";

    protected override void OnClick()
    {
        PlayAnimation(nameAnimation);
    }

    // hàm này sẽ chạy animation
    public void PlayAnimation(string name)
    {
        anim.Play(name);
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
