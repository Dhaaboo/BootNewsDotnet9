namespace BootNews.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Slug { get; set; } = "";
    public List<Article> Articles { get; set; } = new();
}

public class Author
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string? Bio { get; set; }
    public List<Article> Articles { get; set; } = new();
}

public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public List<ArticleTag> ArticleTags { get; set; } = new();
}

public class Article
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Slug { get; set; } = "";
    public string Summary { get; set; } = "";
    public string Content { get; set; } = "";
    public string ImageUrl { get; set; } = "";
    public DateTime PublishedAt { get; set; }
    public int Views { get; set; }
    public int CommentCount { get; set; }
    public bool IsHero { get; set; }        // carousel-ka weyn ee bogga hore
    public bool IsHomeGrid { get; set; }    // 4-ta card ee bogga hore
    public bool IsBreaking { get; set; }    // breaking news ticker
    public bool IsPopular { get; set; }     // popular posts sidebar

    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    public int AuthorId { get; set; }
    public Author Author { get; set; } = null!;

    public List<ArticleTag> ArticleTags { get; set; } = new();
}

public class ArticleTag
{
    public int ArticleId { get; set; }
    public Article Article { get; set; } = null!;
    public int TagId { get; set; }
    public Tag Tag { get; set; } = null!;
}
