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
    
    [Route("/")]
    public IActionResult Index()
    {
        return View();
    }

    [Route("/login")]
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    
    [Route("/login")]
    [HttpPost]
    public IActionResult Login(Users user)
    {
        if (ModelState.IsValid)
        {
            var existingUser = _userService.GetUserByEmail(user.Email);
            if (existingUser != null)
            {
                return Redirect("/events");
            }
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        }
        return View();
    }
    
    [Route("/register")]
    [HttpGet]
    public IActionResult SignUp()
    {
        return View();
    }

    [HttpPost]
    [Route("/register")]
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