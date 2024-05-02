public class Item
{
    public string id;
    public float count;

    public Item()
    {
        this.id = "id";
        this.count = 0f;
    }

    public Item(string id, float count)
    {
        this.id = id;
        this.count = count;
    }
}
