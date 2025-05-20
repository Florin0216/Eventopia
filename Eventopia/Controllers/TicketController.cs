using Eventopia.Data;
using Eventopia.Models;
using Eventopia.Services;
using Eventopia.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eventopia.Controllers;

public class TicketController : Controller
{
    
    private readonly ITicketService _ticketService;

    public TicketController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }
    
    [HttpGet("/createTicket")]
    [Authorize(Roles = "Organizer")]
    public async Task<IActionResult> TicketCreate(int eventId)
    {
        var tickets = await _ticketService.GetTicketsForEvent(eventId);
        ViewBag.EventId = eventId;
        ViewBag.Tickets = tickets;
        return View();
    }

    [HttpPost("/createTicket")]
    [Authorize(Roles = "Organizer")]
    public async Task<IActionResult> TicketCreate(Ticket ticket,int eventId, string action)
    {
        if (!ModelState.IsValid) return View(ticket);
        await _ticketService.Create(ticket, eventId);
        switch (action)
        {
            case "AddAnotherTicket":
                return RedirectToAction("TicketCreate", new { eventId = eventId });
            case "Finish":
                return RedirectToAction("OrganizerDash", "User");
        }
        return View(ticket);
    }
}