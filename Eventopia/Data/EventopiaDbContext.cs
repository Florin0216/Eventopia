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
    public DbSet<TicketUser> TicketUsers { get; set; }
    public DbSet<EventUser> EventUsers { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Checkout> Checkouts { get; set; }
    public DbSet<Venue> Venues { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<CheckoutTicket> CheckoutTickets { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EventUser>()
            .HasKey(eu => new { eu.EventId, eu.userId }); 

        modelBuilder.Entity<CheckoutTicket>()
            .HasKey(ct => new { ct.checkoutId, ct.ticketId });

        modelBuilder.Entity<TicketUser>()
            .HasKey(tu => new { tu.ticketId, tu.userId });

        modelBuilder.Entity<Microsoft.AspNetCore.Identity.IdentityUserLogin<string>>()
            .HasKey(l => new { l.LoginProvider, l.ProviderKey });

        modelBuilder.Entity<Microsoft.AspNetCore.Identity.IdentityUserRole<string>>()
            .HasKey(r => new { r.UserId, r.RoleId });

        modelBuilder.Entity<Microsoft.AspNetCore.Identity.IdentityUserToken<string>>()
            .HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
    }

}