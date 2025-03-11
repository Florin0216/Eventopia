using Eventopia.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Eventopia.Services;

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
    public async Task<IActionResult> Login(Users user)
    {
        if (ModelState.IsValid)
        {
            var result = await _userService.Login(user.UserName, user.PasswordHash, false, false);
            if (result.Succeeded)
            {
                return Redirect("/events");
            }
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
        if (ModelState.IsValid)
        {
            var roles = new List<string> { "User" };
            
            if (!string.IsNullOrEmpty(Role))
            {
                roles.Add("Organizer");
            }
            var result = await _userService.Register(user,user.PasswordHash, roles);
            
            if (result.Succeeded)
            {
                return Redirect("/login");
            }
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