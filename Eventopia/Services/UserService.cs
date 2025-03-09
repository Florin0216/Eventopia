using Eventopia.Data;
using Eventopia.Models;

namespace Eventopia.Services;

public class UserService
{
    private readonly EventopiaDbContext _context;

    public UserService(EventopiaDbContext context)
    {
        _context = context;
    }

    public void AddUser(Users user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public Users GetUserByEmail(string email)
    {
        return _context.Users.FirstOrDefault(u => u.Email == email);
    }
}