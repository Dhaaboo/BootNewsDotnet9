using BootNews.Data;
using BootNews.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BootNews.Controllers;

public class CategoryController : BaseController
{
    public CategoryController(NewsDbContext db) : base(db) { }

    // /Category/business
    [Route("Category/{slug}")]
    public IActionResult Index(string slug)
    {
        var category = Db.Categories.FirstOrDefault(c => c.Slug == slug.ToLower());
        if (category == null) return NotFound();

        var articles = Db.Articles
            .Include(a => a.Author)
            .Where(a => a.CategoryId == category.Id)
            .OrderByDescending(a => a.PublishedAt)
            .ToList();

        var articleIds = articles.Select(a => a.Id).ToList();
        var tags = Db.ArticleTags
            .Include(at => at.Tag)
            .Where(at => articleIds.Contains(at.ArticleId))
            .Select(at => at.Tag)
            .Distinct()
            .ToList();

        var model = new CategoryViewModel
        {
            Category = category,
            Articles = articles,
            Tags = tags,
            Latest = LatestNews(),
            Popular = PopularNews()
        };

        return View(model);
    }
}
