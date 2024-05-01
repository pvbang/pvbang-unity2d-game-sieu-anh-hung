using Firebase.Auth;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using System.Threading.Tasks;

public class _Items : MonoBehaviour
{
    private string ServerID;

    public string NameItem;
    public TextMeshProUGUI TextObject;

    DatabaseReference referenceItems;


    private void Awake()
    {
        ServerID = PlayerPrefs.GetString("ServerID");
    }

    private void Start()
    {
        if (FirebaseAuth.DefaultInstance.CurrentUser == null) return;
        if (ServerID == "") return;

        referenceItems = FirebaseDatabase.DefaultInstance.GetReference("accounts").Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId).Child("servers").Child(ServerID).Child("items");
        referenceItems.ValueChanged += HandleValueChangedItems;
    }

    // lấy thông tin nhân vật mỗi khi có thay đổi trong items
    void HandleValueChangedItems(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }

        // lấy dữ liệu
        DataSnapshot snapshot = args.Snapshot;
        _ = SetItems(snapshot);
    }

    // set thông tin item
    public async Task SetItems(DataSnapshot data)
    {
        string _value = "0";

        if (!data.Child(NameItem).Exists)
        {
            await referenceItems.Child(NameItem).SetValueAsync(0);
        } 
        else
        {
            _value = data.Child(NameItem).Value.ToString();
        }

        TextObject.text = _value;
    }

}
