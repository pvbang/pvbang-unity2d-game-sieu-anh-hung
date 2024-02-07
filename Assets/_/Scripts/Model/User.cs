public class User
{
    public string userID;
    public string userName;
    public string userPassword;

    public User()
    {
        this.userID = "userID";
        this.userName = "userName";
        this.userPassword = "userPassword";
    }

    public User(string userID, string userName, string userPassword)
    {
        this.userID = userID;
        this.userName = userName;
        this.userPassword = userPassword;
    }
}
