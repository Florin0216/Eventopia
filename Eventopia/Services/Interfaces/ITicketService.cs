using Eventopia.Models;

namespace Eventopia.Services.Interfaces;

public interface ITicketService
{
    Task Create(Ticket ticket, int eventId);
    Task<List<Ticket>> GetTicketsForEvent(int eventId);
    IQueryable<Ticket> GetTicketByType(string ticketType);
    Task UpdateTicket(Ticket ticket);
}