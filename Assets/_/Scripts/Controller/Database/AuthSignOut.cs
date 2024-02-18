using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AuthSignOut : BaseButton
{
    protected override void OnClick()
    {
        if (GetComponent<ButtonAnimation>())
        {
            // nếu có animation thì delay theo thời gian của animation
            StartCoroutine(CoroutineHelper.DelaySeconds(() => {
                FirebaseAuth.DefaultInstance.SignOut();
                SceneManager.LoadScene("LoadScene");
            }, GetComponent<ButtonAnimation>().GetLengthAnimation()));
        }
        else
        {
            FirebaseAuth.DefaultInstance.SignOut();
            SceneManager.LoadScene("LoadScene");
        }
    }
}
