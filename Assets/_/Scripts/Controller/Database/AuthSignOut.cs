using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuthSignOut : BaseButton
{
    protected override void OnClick()
    {
        FirebaseConnection.instance.auth.SignOut();
    }
}
