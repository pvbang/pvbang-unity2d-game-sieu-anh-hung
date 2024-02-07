using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LeftHero : MonoBehaviour
{
    public StatusBar statusBar;
    public float currentHP;
    public float totalHP = 10;

    void Start()
    {
        currentHP = totalHP;
        statusBar.updateHP(currentHP, totalHP);
    }

    // Update is called once per frame
    /*void Update()
    {
        
    }*/

    private void OnMouseDown()
    {
        if(currentHP <= 0)
        {
            Destroy(this.gameObject);
        }
        currentHP -= 1;
        statusBar.updateHP(currentHP, totalHP);
    }
}
