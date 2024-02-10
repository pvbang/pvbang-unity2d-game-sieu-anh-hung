using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Level : MonoBehaviour
{
    public TextMeshProUGUI txtLevel;

    private void Awake()
    {
        txtLevel = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateLevel(string level)
    {
        txtLevel.text = level;
    }
}
