public class Server
{
    public string id;
    public string name;
    public string status;

    public Server()
    {
        this.id = "id";
        this.name = "name";
        this.status = "status";
    }

    public Server(string id, string name, string status)
    {
        this.id = id;
        this.name = name;
        this.status = status;
    }
}
