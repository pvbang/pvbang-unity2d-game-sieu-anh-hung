public class Server
{
    public string id;
    public string name;
    public string status;
    public int quantity;

    public Server()
    {
        this.id = "id";
        this.name = "name";
        this.status = "status";
        this.quantity = 0;
    }

    public Server(string id, string name, string status, int quantity)
    {
        this.id = id;
        this.name = name;
        this.status = status;
        this.quantity = quantity;
    }
}
