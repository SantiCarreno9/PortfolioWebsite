namespace BlazorApp.Models;

public class Project
{
    public string Id { get; set; }
    public string? Title { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public ProjectCategory? Category { get; set; } = ProjectCategory.All;
    public Dictionary<int, List<string>>? MediaContent { get; set; } = new Dictionary<int, List<string>>();
    public List<string>? Keywords { get; set; } = new List<string>();

    public Project()
    {
        Id = Guid.NewGuid().ToString();
    }
}

public class MediaType
{
    public int ContentType { get; set; }
    public List<string> URLs { get; set; }
}