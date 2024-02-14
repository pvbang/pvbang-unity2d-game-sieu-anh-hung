using Firebase.Database;
using Firebase.Auth;
using UnityEngine;
using System;

public class FirebaseConnection : MonoBehaviour
{
    public static FirebaseConnection instance;

    public DatabaseReference databaseReference;

    private void Awake()
    {
        FirebaseConnection.instance = this;
        this.databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    //FirebaseAuth.DefaultInstance.CurrentUser
}
