using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroList
{
    public string userID;
    public string userName;
    public string userPassword;

    public HeroList()
    {
        this.userID = "userID";
        this.userName = "userName";
        this.userPassword = "userPassword";
    }

    public HeroList(string userID, string userName, string userPassword)
    {
        this.userID = userID;
        this.userName = userName;
        this.userPassword = userPassword;
    }
}
