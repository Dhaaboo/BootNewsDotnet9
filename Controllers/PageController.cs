using BootNews.Data;
using Microsoft.AspNetCore.Mvc;

namespace BootNews.Controllers;

public class PageController : BaseController
{
    public PageController(NewsDbContext db) : base(db) { }

    public IActionResult About() => View();
    public IActionResult Contact() => View();

    [HttpPost]
    public IActionResult Contact(string name, string email, string message)
    {
        // Demo: halkan waxaad ku dari kartaa keydin database ama email
        ViewBag.Sent = true;
        return View();
    }
}
