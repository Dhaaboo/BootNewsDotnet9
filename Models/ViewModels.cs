namespace BootNews.Models;

public class HomeViewModel
{
    public List<Article> Hero { get; set; } = new();
    public List<Article> Grid { get; set; } = new();
    public List<Article> Sports { get; set; } = new();
    public List<Article> Popular { get; set; } = new();
    public List<Article> Latest { get; set; } = new();
}

public class CategoryViewModel
{
    public Category Category { get; set; } = null!;
    public List<Tag> Tags { get; set; } = new();
    public List<Article> Articles { get; set; } = new();
    public List<Article> Latest { get; set; } = new();
    public List<Article> Popular { get; set; } = new();
}

public class ArticleViewModel
{
    public Article Article { get; set; } = null!;
    public List<string> Tags { get; set; } = new();
    public List<Article> Related { get; set; } = new();
    public List<Article> Latest { get; set; } = new();
    public List<Article> Popular { get; set; } = new();
}

public class SearchViewModel
{
    public string Query { get; set; } = "";
    public List<Article> Results { get; set; } = new();
}

public class SidebarData
{
    public List<Article> Latest { get; set; } = new();
    public List<Article> Popular { get; set; } = new();
}
