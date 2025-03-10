using Eventopia.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Eventopia.Controllers;

public class HomeController : Controller
{
    private readonly SignInManager<Users> _signInManager;
    private readonly UserManager<Users> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public HomeController(SignInManager<Users> signInManager, UserManager<Users> userManager, RoleManager<IdentityRole> roleManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _roleManager = roleManager;
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
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user.UserName, user.PasswordHash, isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return Redirect("/events");
                }
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
    public async Task<IActionResult> SignUp(Users user, string Role)
    {
        if (ModelState.IsValid)
        {
            var result = await _userManager.CreateAsync(user, user.PasswordHash);

            if (result.Succeeded)
            {
                if (await _roleManager.RoleExistsAsync("User"))
                {
                    await _userManager.AddToRoleAsync(user, "User");
                }
                
                if (Role == "Organizer" && await _roleManager.RoleExistsAsync("Organizer"))
                {
                    await _userManager.AddToRoleAsync(user, "Organizer");
                }

                return Redirect("/login");
            }
        }

        return View(user);
    }


    
    [Route("/logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Redirect("/login");
    }

}