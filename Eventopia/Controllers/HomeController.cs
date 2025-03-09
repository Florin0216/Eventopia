using Eventopia.Models;
using Eventopia.Services;
using Microsoft.AspNetCore.Mvc;

namespace Eventopia.Controllers;

public class HomeController : Controller
{
    private readonly UserService _userService;

    public HomeController(UserService userService)
    {
        _userService = userService;
    }
    
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }
    
    [HttpGet]
    public IActionResult SignUp()
    {
        return View();
    }

    [HttpPost]
    public IActionResult SignUp(Users user)
    {
        if (ModelState.IsValid)
        {
            _userService.AddUser(user);
            return RedirectToAction("Login");
        }
        return View(user);
    }
    
}