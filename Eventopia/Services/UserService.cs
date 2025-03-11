using Eventopia.Data;
using Eventopia.Models;
using Microsoft.AspNetCore.Identity;

namespace Eventopia.Services;

public class UserService
{
    private readonly SignInManager<Users> _signInManager;
    private readonly UserManager<Users> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserService(SignInManager<Users> signInManager, UserManager<Users> userManager, RoleManager<IdentityRole> roleManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<IdentityResult> Register(Users user, string password, List<string> roles)
    {
        var result =  await _userManager.CreateAsync(user, password);
        
        if (result.Succeeded)
        {
            foreach (var role in roles)
            {
                await _userManager.AddToRoleAsync(user, role);
            }
        }
        
        return result;
    }
    
    public async Task<SignInResult> Login(string username, string password, bool isPersistent,
        bool lockoutOnFailure)
    {
        return await _signInManager.PasswordSignInAsync(username, password, isPersistent, lockoutOnFailure);
    }

    public async Task Logout()
    {
        await _signInManager.SignOutAsync();
    }
}