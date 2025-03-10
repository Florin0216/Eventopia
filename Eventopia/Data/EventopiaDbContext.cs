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
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<TicketEvent> TicketEvents { get; set; }
    public DbSet<TicketUser> TicketUsers { get; set; }

}