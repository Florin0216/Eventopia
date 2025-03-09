using Eventopia.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Eventopia.Data;

public class EventopiaDbContext : IdentityDbContext<Users>
{
    public EventopiaDbContext(DbContextOptions<EventopiaDbContext> options)
    : base(options)
    {
    }
    
    public DbSet<Users> Users { get; set; }
}