namespace BlazorApp.Models;

public class Project
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<string>? BulletItems { get; set; }
    public Dictionary<int,List<string>>? MediaContent { get; set; }
    public List<string>? VideosURL { get; set; }    
    public List<string>? ImagesURL { get; set; }    
    public string? RepositoryURL { get; set; }
}

public class MediaType
{
    public int ContentType { get; set; }
    public List<string> URLs { get; set; }
}