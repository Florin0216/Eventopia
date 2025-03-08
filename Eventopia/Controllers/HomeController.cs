using Microsoft.AspNetCore.Mvc;

namespace Eventopia.Controllers;

public class HomeController : Controller
{
    
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }
    
    public IActionResult SignUp()
    {
        return View();
    }
    
}