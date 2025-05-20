using Eventopia.Models;
using Microsoft.AspNetCore.Identity;

namespace Eventopia.Services.Interfaces;

public interface IUserService
{
    Task<IdentityResult> Register(Models.Users user, string password, string role);
    Task<SignInResult> Login(string username, string password, bool isPersistent, bool lockoutOnFailure);
    Task Logout();
    Task<bool> UpdateProfilePicture(string userId, string picturePath);
}