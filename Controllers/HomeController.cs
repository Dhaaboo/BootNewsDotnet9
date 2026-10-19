using BootNews.Data;
using BootNews.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BootNews.Controllers;

public class HomeController : BaseController
{
    public HomeController(NewsDbContext db) : base(db) { }

    public IActionResult Index()
    {
        var all = Db.Articles
            .Include(a => a.Author)
            .Include(a => a.Category)
            .OrderByDescending(a => a.PublishedAt)
            .ToList();

        var model = new HomeViewModel
        {
            Hero = all.Where(a => a.IsHero).ToList(),
            Grid = all.Where(a => a.IsHomeGrid).ToList(),
            Sports = all.Where(a => a.Category.Slug == "sports").Take(5).ToList(),
            Popular = PopularNews(),
            Latest = LatestNews()
        };

        return View(model);
    }

    public IActionResult Search(string q)
    {
        var model = new SearchViewModel { Query = q ?? "" };
        if (!string.IsNullOrWhiteSpace(q))
        {
            model.Results = Db.Articles
                .Include(a => a.Author).Include(a => a.Category)
                .Where(a => a.Title.Contains(q) || a.Summary.Contains(q))
                .OrderByDescending(a => a.PublishedAt)
                .ToList();
        }
        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
