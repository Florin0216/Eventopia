using Eventopia.Data;
using Eventopia.Models;
using Eventopia.Repositories.Interfaces;
using Eventopia.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Eventopia.Services;

public class UserService : IUserService
{
    private readonly SignInManager<Users> _signInManager;
    private readonly UserManager<Users> _userManager;
    private readonly IRepositoryWrapper _repositoryWrapper;

    public UserService(SignInManager<Users> signInManager, UserManager<Users> userManager, IRepositoryWrapper repositoryWrapper)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _repositoryWrapper = repositoryWrapper;
    }

    public async Task<IdentityResult> Register(Users user, string password, string role)
    {
        var result =  await _userManager.CreateAsync(user, password);
        
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, role);
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
    
    public async Task<bool> UpdateProfilePicture(string userId, string picturePath)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return false;
        
        user.ProfilePicturePath = picturePath;
        var result = await _userManager.UpdateAsync(user);
        return result.Succeeded;
    }
    
}