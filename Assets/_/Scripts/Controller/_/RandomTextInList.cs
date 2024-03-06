using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RandomTextInList : MonoBehaviour
{
    public TextMeshProUGUI text;
    public List<string> texts;

    private void OnEnable()
    {
        if (text == null) text = GetComponent<TextMeshProUGUI>();

        int index = Random.Range(0, texts.Count);
        text.text = texts[index];
    }
}
