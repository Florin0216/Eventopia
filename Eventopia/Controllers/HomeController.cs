using Eventopia.Models;
using Microsoft.AspNetCore.Mvc;
using Eventopia.Services;
using Eventopia.Services.Interfaces;

namespace Eventopia.Controllers;

public class HomeController : Controller
{

    private readonly IUserService _userService;
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
    public async Task<IActionResult> Login(Users user)
    {
        if (!ModelState.IsValid) return View(user);
        var result = await _userService.Login(user.UserName, user.PasswordHash, false, false);
        if (result.Succeeded)
        {
            return Redirect("/events");
        }

        return View(user);
    }

    
    [HttpGet]
    [Route("/register")]
    public IActionResult SignUp()
    {
        return View();
    }

    [HttpPost]
    [Route("/register")]
    public async Task<IActionResult> SignUp(Users user, string? Role)
    {
        if (!ModelState.IsValid) return View(user);

        if (Role == null)
        {
            Role = "User";
        }
        
        var result = await _userService.Register(user,user.PasswordHash, Role);
            
        if (result.Succeeded)
        {
            return Redirect("/login");
        }

        return View(user);
    }


    
    [Route("/logout")]
    public async Task<IActionResult> Logout()
    {
        await _userService.Logout();
        return Redirect("/login");
    }

}