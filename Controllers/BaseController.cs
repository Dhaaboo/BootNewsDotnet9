using BootNews.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace BootNews.Controllers;

/// <summary>
/// BaseController — wuxuu dhammaan bogagga u diyaariyaa
/// xogta guud: categories (nav-ka) iyo breaking news (ticker-ka).
/// </summary>
public abstract class BaseController : Controller
{
    protected readonly NewsDbContext Db;

    protected BaseController(NewsDbContext db) => Db = db;

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        ViewBag.Categories = Db.Categories.OrderBy(c => c.Name).ToList();
        ViewBag.Breaking = Db.Articles
            .Where(a => a.IsBreaking)
            .OrderByDescending(a => a.PublishedAt)
            .ToList();
        base.OnActionExecuting(context);
    }

    protected List<Models.Article> LatestNews(int count = 4) =>
        Db.Articles.Include(a => a.Author).Include(a => a.Category)
            .OrderByDescending(a => a.PublishedAt).Take(count).ToList();

    protected List<Models.Article> PopularNews(int count = 5) =>
        Db.Articles.Include(a => a.Author).Include(a => a.Category)
            .Where(a => a.IsPopular)
            .OrderByDescending(a => a.Views).Take(count).ToList();
}
