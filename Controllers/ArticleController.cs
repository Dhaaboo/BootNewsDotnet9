using BootNews.Data;
using BootNews.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BootNews.Controllers;

public class ArticleController : BaseController
{
    public ArticleController(NewsDbContext db) : base(db) { }

    // /Article/5/slug-name  ama  /Article/Details/5
    [Route("Article/{id:int}/{slug?}")]
    [Route("Article/Details/{id:int}")]
    public IActionResult Details(int id)
    {
        var article = Db.Articles
            .Include(a => a.Author)
            .Include(a => a.Category)
            .Include(a => a.ArticleTags).ThenInclude(at => at.Tag)
            .FirstOrDefault(a => a.Id == id);

        if (article == null) return NotFound();

        // Views-ku kordhiyaa
        article.Views++;
        Db.SaveChanges();

        var model = new ArticleViewModel
        {
            Article = article,
            Tags = article.ArticleTags.Select(at => at.Tag.Name).ToList(),
            Related = Db.Articles.Include(a => a.Author).Include(a => a.Category)
                .Where(a => a.CategoryId == article.CategoryId && a.Id != article.Id)
                .OrderByDescending(a => a.PublishedAt).Take(3).ToList(),
            Latest = LatestNews(),
            Popular = PopularNews()
        };

        return View(model);
    }
}
