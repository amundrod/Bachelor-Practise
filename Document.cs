public class Document
{
    public string Name { get; set; }
    public Content Content { get; set; }

    public Document(string name, Content content)
    {
        Name = name;
        Content = content;
    }
}
