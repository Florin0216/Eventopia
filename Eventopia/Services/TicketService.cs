using Eventopia.Data;
using Eventopia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eventopia.Services;

public class TicketService
{
    private readonly EventopiaDbContext _context;

    public TicketService(EventopiaDbContext context)
    {
        _context = context;
    }

    public async Task CreateTickets(Ticket ticket, int eventId)
    {
        ticket.EventId = eventId;
        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();

    }

    public async Task<List<Ticket>> GetTicketsForEvent(int eventId)
    {
        return await _context.Tickets
            .Where(t => t.EventId == eventId) 
            .ToListAsync();
    }
}