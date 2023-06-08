namespace ApiKeySample.Models;

public class Book
{
    public string ISBN { get; set; }
    
    public string Title { get; set; }
    
    public string Author { get; set; }
    
    public string? Summary { get; set; }
}