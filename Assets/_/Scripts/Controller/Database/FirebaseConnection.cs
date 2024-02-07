using Firebase.Database;
using Firebase.Auth;
using UnityEngine;
using System;

public class FirebaseConnection : MonoBehaviour
{
    public static FirebaseConnection instance;

    public DatabaseReference databaseReference;
    public FirebaseAuth auth;

    private void Awake()
    {
        FirebaseConnection.instance = this;
        this.databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        this.auth = FirebaseAuth.DefaultInstance;
    }
}
