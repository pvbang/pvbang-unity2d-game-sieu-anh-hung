using Firebase.Auth;
using Firebase.Database;
using Google.MiniJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBuy : BaseButton
{
    private string ServerID;
    public ItemCS itemCS;
    DatabaseReference referenceItems;

    private string itemValue = "###";

    private void Awake()
    {
        if (itemCS == null)
        {
            itemCS = GetComponentInParent<ItemCS>();
        }

        ServerID = PlayerPrefs.GetString("ServerID");
    }

    void HandleValueChangedItems(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }

        // lấy dữ liệu
        DataSnapshot snapshot = args.Snapshot;
        itemValue = snapshot.Child(itemCS.GetCostObjectIDName()).Value.ToString();
    }

    protected override void OnClick()
    {
        if (FirebaseAuth.DefaultInstance.CurrentUser == null) return;
        if (ServerID == "") return;

        if (referenceItems == null)
        {
            referenceItems = FirebaseConnection.instance.databaseReference.Child("accounts").Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId).Child("servers").Child(ServerID).Child("items");
            referenceItems.ValueChanged += HandleValueChangedItems;
        }

        StartCoroutine(UpdateValueItem());
    }


    IEnumerator UpdateValueItem()
    {
        while (itemValue == "###")
        {
            yield return new WaitForSeconds(0.1f);
        }

        referenceItems.Child(itemCS.GetCostObjectIDName()).SetValueAsync(float.Parse(itemValue) - float.Parse(itemCS.GetCost()));
    }
}
