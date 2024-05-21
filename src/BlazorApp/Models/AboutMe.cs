namespace BlazorApp.Models;

public class AboutMe
{
    public string Description { get; set; } = string.Empty;
    public Dictionary<string,List<string>> Skills { get; set; } = new();
    public string DetailOrQuote { get; set; } = string.Empty;
}