namespace BlazorApp.Models;

public class Project
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string VideoURL { get; set; } = string.Empty;
    public List<string>? BulletItems { get; set; }
    public string? RepositoryURL { get; set; }
}