using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ID : MonoBehaviour
{
    public string id = "";

    private void Awake()
    {
        if (id == "")
        {
            id = gameObject.name;
        }
    }
}
