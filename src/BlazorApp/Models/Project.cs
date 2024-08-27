namespace BlazorApp.Models;

public class Project
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;    
    public int? Technologies { get; set; }
    public int? Tools { get; set; }    
    public Dictionary<int,List<string>>? MediaContent { get; set; }    
    public List<string>? Keywords { get; set; }    
}

public class MediaType
{
    public int ContentType { get; set; }
    public List<string> URLs { get; set; }
}