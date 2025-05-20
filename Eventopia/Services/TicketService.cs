using Eventopia.Data;
using Eventopia.Models;
using Eventopia.Repositories.Interfaces;
using Eventopia.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eventopia.Services;

public class TicketService : ITicketService
{
    private readonly IRepositoryWrapper _repositoryWrapper;

    public TicketService(IRepositoryWrapper repositoryWrapper)
    {
        _repositoryWrapper = repositoryWrapper;
    }

    public async Task Create(Ticket ticket, int eventId)
    {
        ticket.EventId = eventId;
        _repositoryWrapper.Ticket.Create(ticket);
        await _repositoryWrapper.SaveAsync();

    }

    public async Task<List<Ticket>> GetTicketsForEvent(int eventId)
    {
        return await _repositoryWrapper.Ticket.FindByCondition(e => e.EventId == eventId)
            .ToListAsync();
    }

    public IQueryable<Ticket> GetTicketByType(string ticketType)
    {
        return _repositoryWrapper.Ticket.FindByCondition(t => t.Type == ticketType);
    }
    
    public async Task UpdateTicket(Ticket ticket)
    {
        _repositoryWrapper.Ticket.Update(ticket);
        await _repositoryWrapper.SaveAsync();
    }

}