using Microsoft.AspNetCore.Mvc;

namespace PixieFit.Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Pricing()
    {
        return View();
    }
}

